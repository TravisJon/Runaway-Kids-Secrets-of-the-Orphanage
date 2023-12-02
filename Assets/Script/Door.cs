using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Door : MonoBehaviour
{
    private Inventory inventory;
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (inventory.isUnlock == true)
            {
                GetComponent<BoxCollider2D>().enabled = false;
            }
            else if (inventory.isUnlock == false)
            {
                GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }
}
