using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBoxRotateScript : MonoBehaviour
{
    public Vector3 RotateAmount; //degrees per second to rotate 

    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(RotateAmount * Time.deltaTime);
    }
}
