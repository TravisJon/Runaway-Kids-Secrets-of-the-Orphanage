using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInstructions : MonoBehaviour
{
    public Text instructionsText;
    public float displayTime = 5f;

    private void Start()
    {
        StartCoroutine(DisplayInstructions());
    }

    private IEnumerator DisplayInstructions()
    {
        instructionsText.enabled = true;

        yield return new WaitForSeconds(displayTime);

        instructionsText.enabled = false;

        Debug.Log("Petunjuk hilang. Lakukan hal-hal lain di sini.");
    }
}
