using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    public WaypointFollower enemy;
    public float gameOverDistance = 1.0f;
    public string gameOverSceneName = "GameOverScene"; // Change this to your game over scene name
    public GameObject gameOverScreen;

    // Update is called once per frame
    void Update()
    {
        if (IsGameOver())
        {
            HandleGameOver();
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

    void HandleGameOver()
    {
        gameOverScreen.SetActive(true);
        // You can add additional game over logic here, such as showing a game over screen, playing a sound, etc.
        // For now, let's just reload the specified scene.
        SceneManager.LoadScene("Scene 1");
    }
}
