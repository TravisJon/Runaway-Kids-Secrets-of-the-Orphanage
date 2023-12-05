using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorLock : MonoBehaviour
{
    private Inventory inventory;
    [SerializeField] private bool tryOpenAllowed;
    [SerializeField] private string sceneToLoad;
    
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

    }
    private void Update()
    {
        if (tryOpenAllowed && Input.GetKeyDown(KeyCode.R))
        {
            tryingOpen();
        }
    }

    private void tryingOpen()
    {
        if (inventory.isUnlock == true)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            tryOpenAllowed = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            tryOpenAllowed = false;
        }
    }
}
