using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressInteract : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject handImage;
    [SerializeField] private bool isInteracting;
    public float holdDuration = 3f;
    public Image fillCircle;

    public float holdTimer = 0;
    public bool isHolding = false;
    public GameObject destroyer;
    void Start()
    {

    }
    void Update()
    {
        onHold();

        if (isHolding)
        {
            holdTimer += Time.deltaTime;
            fillCircle.fillAmount = Mathf.Clamp01(holdTimer / holdDuration);

            if (holdTimer >= holdDuration)
            {
                Destroy(destroyer);
            }
        }
    }

    public void onHold()
    {
        if (isInteracting && Input.GetKeyDown(KeyCode.F)) 
        {
            isHolding = true;
        }
        else if (isInteracting && (Input.GetKeyUp(KeyCode.F) || !Input.GetKey(KeyCode.F)))
        {
            resetHold();
        }
    }

    public void resetHold()
    {
        isHolding = false;
        holdTimer = 0;
        fillCircle.fillAmount = 0;
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
