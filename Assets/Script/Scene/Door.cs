using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private bool tryOpenAllowed;
    [SerializeField] private string sceneToLoad;

    [SerializeField] private GameObject handImage;
    [SerializeField] private bool isInteracting;

    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        if (tryOpenAllowed && isInteracting && Input.GetKeyDown(KeyCode.E))
        {
            if (SceneManager.GetActiveScene().name == "Scene 2")
            {
                InGameDatabase.instance.playerPosition = player.position;
            }
            
            SceneManager.LoadScene(sceneToLoad);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            player = collision.gameObject.transform;

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
