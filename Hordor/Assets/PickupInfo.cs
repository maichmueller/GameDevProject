using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickupInfo : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public List<GameObject> prefabs;
    private Camera mainCamera;
    private GameObject currInfo;
    private int _state = 0;


    private List<Vector3> spawnLocations = new List<Vector3>();

    private void Start()
    {
        player.GetComponent<CharacterController>().enabled = false;
        currInfo = transform.GetChild(0).gameObject;
        mainCamera = player.transform.GetChild(0).gameObject.GetComponent<Camera>();
        SetActiveChild(0);
        spawnLocations.Add(enemy.transform.position + new Vector3(-5, 0, 0));
        spawnLocations.Add(enemy.transform.position + new Vector3(5, 0, 0));
        player.GetComponent<Health>().DeathEvent += OnDeath;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_state);
        if (Input.GetKeyDown(KeyCode.Space) && _state == 0)
        {
            var controller = player.GetComponent<CharacterController>();
            player.GetComponent<CharacterController>().enabled = true;
            var moveDirection = Vector3.zero;
            if (controller.isGrounded)
            {
                moveDirection.y = 8f;
            }

            moveDirection.y -= 9.81f * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);

            // moving from spacebar hint to moving towards enemey
            SetActiveChild(1);
        }

        else if ((player.transform.position - enemy.transform.position).magnitude <=
                 player.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Gun>().weaponRange)
        {
            switch (_state)
            {
                case 1:
                {
                    // moving from move towards enemey hint to pickup hint
                    SetActiveChild(2);
                    break;
                }
                case 2:
                {
                    RaycastHit hit;
                    if (Input.GetMouseButtonDown(0) && Physics.Raycast(CenterRay(), 500))
                    {
                        // pickup hint -> release hint
                        SetActiveChild(3);
                    }

                    break;
                }
                case 3:
                {
                    StartCoroutine("WaitAndKill", 5f);
                    break;
                }
                default:
                {
                    break;
                }
            }
        }
        else if (_state == 4)
        {
            prefabs[0].SetActive(true);
            prefabs[1].SetActive(true);

            _state = 5;
        }
        else if (_state > 0 && _state < 3)
        {
            SetActiveChild(1);
        }

        if (!prefabs[0].GetComponent<Health>().alive && !prefabs[1].GetComponent<Health>().alive)
        {
            StartCoroutine("WaitAndReturn", 2);
        }
    }


    void OnDeath()
    {
        _state = -1;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    IEnumerator WaitAndKill(float seconds)
    {
        float elapsed = 0f;
        while (elapsed < seconds)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        if (_state != 4)
        {
            SetActiveChild(4);
        }
    }

    IEnumerator WaitAndReturn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("Scenes/Menu");
    }


    void SetActiveChild(int index)
    {
        int i = 0;
        foreach (Transform child in transform)
        {
            if (i == index)
            {
                currInfo = child.gameObject;
                currInfo.SetActive(true);
                _state = i;
            }
            else
            {
                child.gameObject.SetActive(false);
            }

            i++;
        }
    }

    private Ray CenterRay()
    {
        return mainCamera.ViewportPointToRay(Vector3.one * 0.5f);
    }
}