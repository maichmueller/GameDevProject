using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Respawn : MonoBehaviour
{
    public Vector3 spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.position = spawnPoint;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RespawnObject()
    {
        this.gameObject.transform.position = spawnPoint;
        this.gameObject.SetActive(true);
    }
}
