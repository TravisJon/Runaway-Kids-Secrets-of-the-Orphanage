using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressInteract : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject handImage;
    [SerializeField] private bool isInteracting;
    public static float progress;
    void Start()
    {
        ProgressInteract.progress = 1f;
    }
    void Update()
    {
        if (isInteracting && Input.GetKey(KeyCode.F))
        {
            progress += 1f * Time.deltaTime;
            progress = Mathf.Clamp01(progress);
        }
        else
        {
            progress = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            handImage.SetActive(true);
            isInteracting = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            handImage.SetActive(false);
            isInteracting = false;
        }
    }
}
