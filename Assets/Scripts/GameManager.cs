using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Text scoreText;
    public Text highScoreText;
    public GameObject gameOverPanel; // Referencia al panel de Game Over
    public Text countdownText; // Texto para la cuenta atrás en UI
    public AudioSource gameMusic; // Música del juego
    public GameObject obstacleSpawner; // Referencia al spawner de enemigos

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
        gameOverPanel.SetActive(false); // Asegurar que el Game Over Panel esté oculto al inicio
        obstacleSpawner.SetActive(false); // Desactivar enemigos al inicio
        gameMusic.Stop(); // Asegurar que la música no comienza antes de tiempo
        StartCoroutine(StartCountdown()); // Iniciar la cuenta atrás
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
        countdownText.gameObject.SetActive(false); // Ocultar la cuenta atrás

        StartGame();
    }

    void StartGame()
    {
        gameStarted = true;
        obstacleSpawner.SetActive(true); // Activar generación de enemigos
        gameMusic.Play(); // Iniciar la música
    }

    public void AddPoint()
    {
        if (!gameStarted) return;

        score++;
        UpdateUI();
        Vibrate(50);
    }

    public void GameOver()
    {
        if (!gameStarted) return;

        Debug.Log("❌ GAME OVER ❌");
        gameStarted = false;
        obstacleSpawner.SetActive(false); // Detiene los enemigos
        gameMusic.Stop(); // Para la música
        Time.timeScale = 0; // Detener el juego
        gameOverPanel.SetActive(true); // Mostrar pantalla de Game Over
    }

    public void RestartGame()
    {
        Time.timeScale = 1; // Restaurar la velocidad del tiempo
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reiniciar la escena
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
