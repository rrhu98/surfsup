using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInventory : MonoBehaviour
{
    public static GameObject heldItem;
    public GameObject[] itemPrefabs;

    public void setItem()
    {
        GameObject itemtogive = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
        heldItem = itemtogive;
        Debug.Log("setting players item to have " + itemtogive.name);
    }

    public void UsedItem()
    {
        heldItem = null;
    }
    

    // Start is called before the first frame update
    void Start()
    {
        heldItem = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (heldItem != null)
            {
                Instantiate(heldItem, transform.position, transform.rotation);
                UsedItem();
            }
            else
            {
                print("You don't have any items!");
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            print("player currently holding " + heldItem);
        }
    }
}
