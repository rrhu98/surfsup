using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Fragsurf.Movement;

public class TeleportPlayer : NetworkBehaviour  
{
	[SerializeField]
	private Vector3 spawnCoords = new Vector3(-40,300,0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnTriggerEnter(Collider other)
	{
		GameObject player = other.transform.parent.gameObject;
        Transform playerTransform = player.GetComponent<Transform>();
        if (playerTransform != null)
        {
            playerTransform.position = spawnCoords;
        }
    }
}
