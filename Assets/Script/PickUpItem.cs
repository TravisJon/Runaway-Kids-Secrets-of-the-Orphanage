using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private bool pickUpAllowed;
    private Inventory inventory;
    public GameObject items;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pickUpAllowed && Input.GetKeyDown(KeyCode.F)) 
        {
            pickUp();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player")) 
        {
            pickUpAllowed = true;        
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            pickUpAllowed = false;
        }
    }

    private void pickUp()
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.isFull[i] == false)
            {
                inventory.isFull[i] = true;
                Instantiate(items, inventory.slots[i].transform, false);
                Destroy(gameObject);
                break;
            }
        }
    }
    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i <inventory.slots.Length; i++)
            {
                if (inventory.isFull[i] == false)
                {
                    inventory.isFull[i] = true;
                    Instantiate(itemButton, inventory.slots[i].transform, false);
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }*/

}
