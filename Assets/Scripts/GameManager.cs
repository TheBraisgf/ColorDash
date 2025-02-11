using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Text scoreText;
    public Text highScoreText;
    public GameObject gameOverPanel; // Referencia al panel de Game Over

    private int score = 0;
    private int highScore = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        gameOverPanel.SetActive(false); // Asegurar que el Game Over Panel esté oculto al inicio
        UpdateUI();
    }

    public void AddPoint()
    {
        score++;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        UpdateUI();
    }

    public void GameOver()
    {
        Debug.Log("❌ GAME OVER ❌");
        gameOverPanel.SetActive(true); // Mostrar pantalla de Game Over
        Time.timeScale = 0; // Detener el juego
    }

    public void RestartGame()
    {
        Time.timeScale = 1; // Restaurar la velocidad del tiempo
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reiniciar la escena
    }

    void UpdateUI()
    {
        scoreText.text = score.ToString();
        highScoreText.text = highScore.ToString();
    }
}
