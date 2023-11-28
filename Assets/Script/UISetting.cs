using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISetting : MonoBehaviour
{
    public Button saveButton;
    public Button backButton;
    public Slider sliderSound;
    public Slider sliderMusic;

    public GameObject mainMenuScreen;

    public GameObject[] tabGameObjects;

    private int currentTab;

    // Start is called before the first frame update
    void Start()
    {
        saveButton.onClick.AddListener(delegate { SaveSettings(); });
        backButton.onClick.AddListener(delegate { GoBack(); });
        sliderSound.onValueChanged.AddListener(delegate { CheckSettingsChanged(); });
        sliderMusic.onValueChanged.AddListener(delegate { CheckSettingsChanged(); });
    }

    private void OnEnable()
    {
        sliderSound.value = AudioListener.volume;
        sliderMusic.value = AudioListener.volume;
        //loading this values from file/playerprefs should be on starting your game, not in entering settings
        saveButton.interactable = false;
    }

    // Update is called once per frame
    private void SwitchTab(int newTab)
    {
        tabGameObjects[currentTab].SetActive(true);
        tabGameObjects[newTab].SetActive(false);
        currentTab = newTab;
    }

    private void SaveSettings()
    {
        AudioListener.volume = sliderSound.value;
        AudioListener.volume = sliderMusic.value;
        //Generally you should store this setting in file or player prefs so they wont be lost on loading new scene or restarting your game
    }

    private void GoBack()
    {
        gameObject.SetActive(false);
        mainMenuScreen.SetActive(true);
    }

    private void CheckSettingsChanged()
    {
        saveButton.interactable = sliderSound.value != AudioListener.volume;
        saveButton.interactable = sliderMusic.value != AudioListener.volume;
    }
}
