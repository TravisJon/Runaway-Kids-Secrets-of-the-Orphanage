using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credit : MonoBehaviour
{
    public Button backButton;
    public GameObject mainMenuScreen;

    // Start is called before the first frame update
    void Start()
    {
        backButton.onClick.AddListener(delegate { GoBack(); });
    }

    // Update is called once per frame
    private void GoBack()
    {
        gameObject.SetActive(false);
        mainMenuScreen.SetActive(true);
    }
}
