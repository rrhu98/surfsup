using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeRespawn : MonoBehaviour
{
    [SerializeField]
    private Vector3 spawnCoords = new Vector3(-12, 104, 0);
    

    void OnTriggerEnter(Collider other)
    {
        GameObject player = other.transform.parent.gameObject;
        Transform playerTransform = player.GetComponent<Transform>();
        print(playerTransform);
        playerTransform.position = spawnCoords;
    }
}
