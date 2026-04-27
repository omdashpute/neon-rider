using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Reference to the restart button and score tracking
    public GameObject restartButton;
    private int score = 0;
    private int coins = 0;

    // Reference to the score text UI element
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;
    [SerializeField] private TMPro.TextMeshProUGUI coinsText;

    public void SetScore(int score)
    {
        this.score = score;
    }

    public int GetScore()
    {
        return score;
    }

    public void SetCoins(int coins)
    {
        this.coins = coins;
    }

    public int GetCoins()
    {
        return coins;
    }

    // Method to handle game over state
    public void GameOver()
    {
        Time.timeScale = 0;
        restartButton.SetActive(true);
    }

    // Method to restart the game by reloading the current scene
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Method to update the score and display it on the UI
    public void Score()
    {
        score+=10;
        scoreText.text = "Score: " + score;
        SetScore(score);
    }

    public void Coins()
    {
        coins += 1;
        coinsText.text = "Coins: " + coins;
        SetCoins(coins);
    }
}