using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int score; // The player's score
    public int highScore; // The current high score
    public int scoreToWin = 10; // The score required to win the game

    void Start()
    {
        // Load the high score from PlayerPrefs
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    // Add points to the player's score and check for win condition
    public void AddPoints(int points)
    {
        score += points;

        // Update the high score if the player's score is greater
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        // Check for win condition
        if (score >= scoreToWin)
        {
            SceneManager.LoadScene("Win");
        }
    }

    // Penalize the player by deducting points from their score
    public void Penalize(int points)
    {
        score -= points;

        // Clamp the score to a minimum of 0
        if (score < 0)
        {
            score = 0;
        }
    }

    // Update the high score in PlayerPrefs
    public void UpdateHighScore(int score)
    {
        PlayerPrefs.SetInt("HighScore", highScore);
    }
}