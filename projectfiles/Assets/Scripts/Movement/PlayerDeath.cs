using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Fragsurf.Movement;

public class PlayerDeath : NetworkBehaviour
{
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

		SurfCharacter surfChar = player.GetComponent<SurfCharacter>();
		if (surfChar != null)
		{
			surfChar.Death();
		}
	}
}
