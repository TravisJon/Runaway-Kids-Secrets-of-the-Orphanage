using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
     // Start is called before the first frame update
    public WaypointFollower enemy;
    public float gameOverDistance = 2.0f;
    public string gameOverSceneName = "GameOverScene"; // Change this to your game over scene name
    public GameObject gameOverScreen;
    public float gameOverScreenDuration = 2.0f;

    private bool isGameOver = false;

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver && IsGameOver())
        {
            isGameOver = true;
            StartCoroutine(ShowGameOverScreen());
        }
    }

    bool IsGameOver()
    {
        if (enemy != null && Vector2.Distance(enemy.transform.position, enemy.player.transform.position) < gameOverDistance)
        {
            return true;
        }
        return false;
    }

    void CheckAndEndTimer()
    {
        if (TimeController.instance != null)
        {
            TimeController.instance.EndTimer();
        }
    }

    IEnumerator ShowGameOverScreen()
    {
        CheckAndEndTimer();
        gameOverScreen.SetActive(true);

        // Wait for the specified duration
        yield return new WaitForSeconds(gameOverScreenDuration);

        // Reload the specified scene
        SceneManager.LoadScene(gameOverSceneName);
    }
}
