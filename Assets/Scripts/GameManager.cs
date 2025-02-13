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
    public int maxLives = 3;  // 🔥 Número máximo de vidas
    private int currentLives;
    public Image screenOverlay;
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
        currentLives = maxLives;
        gameOverPanel.SetActive(false);
        obstacleSpawner.SetActive(false);
        screenOverlay.color = new Color(0, 0, 0, 0);
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
    public void TakeDamage()
    {
        currentLives--;
        Debug.Log("💥 Golpe recibido! Vidas restantes: " + currentLives);

        if (currentLives == 2)
        {
            CameraShake.Instance.ShakeCamera(0.2f, 0.2f); // 🔥 Pequeño shake de cámara
        }
        else if (currentLives == 1)
        {
            CameraShake.Instance.ShakeCamera(0.4f, 0.3f); // 🔥 Shake más fuerte
            StartCoroutine(DarkenScreen()); // 🔥 Oscurecer pantalla
        }
        else if (currentLives <= 0)
        {
            CameraShake.Instance.ShakeCamera(0.6f, 0.5f); // 🔥 Shake más fuerte en Game Over
            GameOver();
        }
    }

    IEnumerator DarkenScreen()
    {
        float duration = 1f;
        float elapsedTime = 0;
        Color startColor = screenOverlay.color;
        Color targetColor = new Color(0, 0, 0, 0.5f); // 🔥 Oscurece al 50%

        while (elapsedTime < duration)
        {
            screenOverlay.color = Color.Lerp(startColor, targetColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        screenOverlay.color = targetColor; // Asegurar que termine en el color exacto
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

        gameStarted = false;
        obstacleSpawner.SetActive(false);
        gameMusic.Stop();
        Vibrate(500); // Vibración larga al perder
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;

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
        }
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1; // Asegurar que el juego no esté pausado
        SceneManager.LoadScene("MainMenu");
    }

    public void HitPause(float duration)
    {
        StartCoroutine(Pause(duration));
    }

    private IEnumerator Pause(float duration)
    {
        Time.timeScale = 0.1f; // Ralentiza el tiempo
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f; // Vuelve a la normalidad
    }

}
