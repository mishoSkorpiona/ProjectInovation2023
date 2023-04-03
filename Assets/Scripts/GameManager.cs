using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float timeLimit = 60.0f; // The time limit for the game
    public int score = 0; // The player's score
    public int penalty = 5; // The penalty for dropping a plate
    public GameObject gameOverPanel; // The panel to display when the game is over

    private float currentTime = 0.0f; // The current time elapsed in the game
    private bool gameOver = false; // Flag to indicate if the game is over
    public GameController gameController; // The GameController object

    void Start()
    {
        Time.timeScale = 1.0f; // Reset the time scale to 1
    }

    void Update()
    {
        if (!gameOver)
        {
            currentTime += Time.deltaTime; // Update the current time elapsed
            if (currentTime >= timeLimit) // Check if the time limit has been reached
            {
                EndGame();
            }
        }
    }

    // End the game by displaying the game over panel and updating the GameController object
    private void EndGame()
    {
        gameOver = true;
        Time.timeScale = 0.0f; // Pause the game
        gameController.enabled = false; // Disable the GameController object
        gameOverPanel.SetActive(true); // Display the game over panel
        gameController.UpdateHighScore(score); // Update the high score
    }

    // Add points to the player's score
    public void AddPoints(int points)
    {
        score += points;
    }

    // Subtract points from the player's score as a penalty for dropping a plate
    public void Penalize()
    {
        score -= penalty;
    }

    // Restart the game by reloading the current scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Quit the game
    public void QuitGame()
    {
        Application.Quit();
    }
}