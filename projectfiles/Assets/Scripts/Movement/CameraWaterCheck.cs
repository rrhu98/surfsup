using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CameraWaterCheck : NetworkBehaviour {
    
    private List<Collider> triggers = new List<Collider> ();

    private void OnTriggerEnter (Collider other) {
        
        if (!triggers.Contains (other))
            triggers.Add (other);

    }

    private void OnTriggerExit (Collider other) {
        
        if (triggers.Contains (other))
            triggers.Remove (other);

    }

    public bool IsUnderwater () {
        
        foreach (Collider trigger in triggers) {

            if (trigger.GetComponentInParent<Water> ())
                return true;

        }

        return false;

    }

}
