using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public float durationOfDeathBehaviour = 0.5f;
    public Vector3 endRotationEulers = new Vector3(0, 0, 85f);
    public Vector3 endPositionChange = new Vector3(0, -.1f, 0);
    private Vector3 endPosition;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Health>().DeathEvent += OnDeath;
    }

    // Update is called once per frame
    void OnDeath()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Immobilize();
        endPosition = transform.position + endPositionChange;
        StartCoroutine(Fall());
    }

    void Immobilize()
    {
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        this.gameObject.GetComponent<QuakeMovement>().enabled = false;
        this.gameObject.GetComponent<PlayerController>().enabled = false;
    }

    IEnumerator Fall()
    {
        var transform = this.gameObject.transform;
        while ((transform.localRotation.eulerAngles - endRotationEulers).magnitude > 0.01f)
        {
            // transform.localPosition = Vector3.Lerp(transform.localPosition, endPosition,
                // durationOfDeathBehaviour * Time.deltaTime);
            transform.localRotation = Quaternion.Euler(Vector3.Lerp(transform.localRotation.eulerAngles,
                endRotationEulers,
                durationOfDeathBehaviour * Time.deltaTime));
            yield return null;
        }
    }
}