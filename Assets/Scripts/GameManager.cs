using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Text scoreText;
    public Text highScoreText;
    public GameObject gameOverPanel;
    public Text countdownText;
    public AudioSource gameMusic;
    public GameObject obstacleSpawner;
    public AudioSource collectSound; // Sonido al recoger obstáculos

    private int score = 0;
    private bool gameStarted = false;

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
        gameOverPanel.SetActive(false);
        obstacleSpawner.SetActive(false);
        gameMusic.Stop();
        StartCoroutine(StartCountdown());
        UpdateUI();
    }

    IEnumerator StartCountdown()
    {
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        countdownText.text = "GO!";
        yield return new WaitForSeconds(0.5f);
        countdownText.gameObject.SetActive(false);

        StartGame();
    }

    void StartGame()
    {
        gameStarted = true;
        obstacleSpawner.SetActive(true);
        gameMusic.Play();
    }

    public void AddPoint()
    {
        if (!gameStarted) return;

        score++;
        UpdateUI();

        if (collectSound != null)
            collectSound.Play(); // Sonido al recoger un objeto

        Vibrate(50);
    }

    public void GameOver()
    {
        if (!gameStarted) return;

        Debug.Log("❌ GAME OVER ❌");
        gameStarted = false;
        obstacleSpawner.SetActive(false);
        gameMusic.Stop();
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);

        Vibrate(500); // Vibración larga al perder
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void UpdateUI()
    {
        scoreText.text = score.ToString();
    }

    public void Vibrate(int milliseconds)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            Handheld.Vibrate();
            Debug.Log("Vibrando por " + milliseconds + " milisegundos");
        }
    }
}
