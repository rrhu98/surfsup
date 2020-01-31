using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fragsurf.Movement;
using Mirror;

public class Booster : NetworkBehaviour
{
    /// Force impulse depends on rigit body mass
    [SerializeField]
    float velocityMultiplier = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter( Collider other )
    {
        GameObject player = other.transform.parent.gameObject;
        Quaternion playerRotation = player.GetComponent<Transform>().rotation;
        SurfCharacter surfCharacter = player.GetComponent<SurfCharacter>();
        if (surfCharacter != null)
        {
            //surfCharacter.ResetPosition();
            surfCharacter.moveData.velocity *= velocityMultiplier;
        }
    }

    private void OnTriggerStay(Collider other)
    {
    }
}
