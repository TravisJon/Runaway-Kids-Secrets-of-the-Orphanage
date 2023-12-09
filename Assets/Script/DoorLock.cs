using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorLock : MonoBehaviour
{
    private Inventory inventory;
    [SerializeField] private bool tryOpenAllowed;
    [SerializeField] private string sceneToLoad;

    [SerializeField] private GameObject handImage;
    [SerializeField] private bool isInteracting;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

    }
    private void Update()
    {
        if (tryOpenAllowed && isInteracting && Input.GetKeyDown(KeyCode.E))
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
            handImage.SetActive(true);
            isInteracting = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            tryOpenAllowed = false;
            handImage.SetActive(false);
            isInteracting = false;
        }
    }
}
