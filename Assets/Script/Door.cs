using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private bool tryOpenAllowed;
    [SerializeField] private string sceneToLoad;

    public GameObject handImage;
    private bool isInteracting;

    // Start is called before the first frame update
    void Start()
    {
        handImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (tryOpenAllowed && isInteracting && Input.GetKeyDown(KeyCode.R))
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
