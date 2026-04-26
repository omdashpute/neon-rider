using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject restartButton;
    private int score = 0;

    [SerializeField] private TMPro.TextMeshProUGUI scoreText;

    public void GameOver()
    {
        Time.timeScale = 0;
        restartButton.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Score()
    {
        score+=10;
        scoreText.text = "Score: " + score;
    }
}