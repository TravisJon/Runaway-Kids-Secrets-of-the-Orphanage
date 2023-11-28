using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIMainMenu : MonoBehaviour
{
    public Button continueButton;
    public Button newGameButton;
    public Button settingsButton;
    public Button quitButton;

    public GameObject SettingsScreen;

    // Start is called before the first frame update
    void Start()
    {
        continueButton.onClick.AddListener(delegate { ContinueGame(); });
        newGameButton.onClick.AddListener(delegate { StartNewGame(); });
        settingsButton.onClick.AddListener(delegate { OpenSettings(); });
        quitButton.onClick.AddListener(delegate { QuitGame(); });
    }

    // Update is called once per frame
    private void OnEnable()
    {
        CheckForSavedGame();
    }

    private void CheckForSavedGame()
    {
        //example
        continueButton.gameObject.SetActive(System.IO.File.Exists(Application.persistentDataPath + "/ExampleFileSaveName.save"));
    }

    private void ContinueGame()
    {
        //load save and for example load scene
    }

    private void StartNewGame()
    {
        //load your scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OpenSettings()
    {
        gameObject.SetActive(false);
        SettingsScreen.SetActive(true);
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
