using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideInCloset : MonoBehaviour
{
    public GameObject player;
    private Vector2 originalPlayerPosition; 
    private Vector2 originalPlayerScale;
    public KeyCode hideKey = KeyCode.E; 
    public float interactionRange = 2f; 
    private bool isHiding = false;

    private Animator closetAnimator;

    public GameObject handInteraction;

    void Start()
    {
        originalPlayerPosition = player.transform.position;
        originalPlayerScale = player.transform.localScale;

        closetAnimator = GetComponent<Animator>();
        handInteraction.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(hideKey) && !isHiding && IsPlayerNearby())
        {
            StartCoroutine(HidePlayerCoroutine());
        }
        else if (Input.GetKeyDown(hideKey) && isHiding)
        {
            StartCoroutine(UnhidePlayerCoroutine());
        }

        if (IsPlayerNearby())
        {
            handInteraction.SetActive(true);
        }
        else
        {
            handInteraction.SetActive(false);
        }
    }

    bool IsPlayerNearby()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        return distance <= interactionRange;
    }

    IEnumerator HidePlayerCoroutine()
    {
        originalPlayerPosition = player.transform.position;
        closetAnimator.SetBool("IsOpen", true);

        yield return new WaitForSeconds(closetAnimator.GetCurrentAnimatorStateInfo(0).length);
        player.transform.position = transform.position;
        isHiding = true;

        player.transform.localScale = new Vector2(0.5f, 0f);
    }

    IEnumerator UnhidePlayerCoroutine()
    {
        player.transform.position = originalPlayerPosition;
        isHiding = false;
        player.transform.localScale = originalPlayerScale;
        closetAnimator.SetBool("IsOpen", false);

        yield return new WaitForSeconds(closetAnimator.GetCurrentAnimatorStateInfo(0).length);
    }
}
