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
    public AudioSource countdownBeep; // 🔊 Sonido en cada número del countdown
    public AudioSource countdownGoSound; // 🔊 Sonido especial en "GO!"
    public AudioSource damageSound; // 🔊 Sonido al recibir daño
    public AudioSource gameOverSound; // 🔊 Sonido en Game Over

    public Text finalScoreText;
    public Text gameOverMessage;
    public Button watchAdButton; // Botón para ver anuncio y continuar

    private int score = 0;
    private bool gameStarted = false;
    public int maxLives = 3;  // 🔥 Número máximo de vidas
    private int currentLives;
    private int highScore;
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

        // Cargar High Score al inicio
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        StartCoroutine(StartCountdown());
        UpdateUI();
    }

    IEnumerator StartCountdown()
    {
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            if (countdownBeep != null) countdownBeep.Play();
            yield return new WaitForSeconds(1f);
        }

        countdownText.text = "GO!";
        if (countdownGoSound != null) countdownGoSound.Play();
        yield return new WaitForSeconds(0.5f);
        countdownText.gameObject.SetActive(false);

        StartGame();
    }

    public void TakeDamage()
    {
        currentLives--;
        Debug.Log("💥 Golpe recibido! Vidas restantes: " + currentLives);
        if (currentLives > 0)
        {
            if (damageSound != null) damageSound.Play(); // 🔊 Sonido de daño
        }
        if (currentLives == 2)
        {
            CameraShake.Instance.ShakeCamera(0.2f, 0.2f);
        }
        else if (currentLives == 1)
        {
            CameraShake.Instance.ShakeCamera(0.4f, 0.3f);
            StartCoroutine(DarkenScreen());
        }
        else if (currentLives <= 0)
        {
            CameraShake.Instance.ShakeCamera(0.6f, 0.5f);
            GameOver();
        }
    }

    IEnumerator DarkenScreen()
    {
        float duration = 1f;
        float elapsedTime = 0;
        Color startColor = screenOverlay.color;
        Color targetColor = new Color(0, 0, 0, 0.5f);

        while (elapsedTime < duration)
        {
            screenOverlay.color = Color.Lerp(startColor, targetColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        screenOverlay.color = targetColor;
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
            collectSound.Play();

        Vibrate(50);
    }

    public void GameOver()
    {
        if (!gameStarted) return;

        gameStarted = false;
        obstacleSpawner.SetActive(false);
        gameMusic.Stop();

        if (gameOverSound != null) gameOverSound.Play();
        Vibrate(500);
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;

        // Mostrar puntuación final animada
        StartCoroutine(AnimateFinalScore());
    }

    IEnumerator AnimateFinalScore()
    {
        int displayedScore = 0;
        finalScoreText.text = "0";
        while (displayedScore < score)
        {
            displayedScore++;
            finalScoreText.text = "" + displayedScore;
            yield return new WaitForSeconds(0.05f);
        }

        // Mostrar mensaje según la diferencia con el HighScore
        if (score > highScore)
        {
            gameOverMessage.text = "🔥 ¡Nuevo récord! Eres increíble!";
            PlayerPrefs.SetInt("HighScore", score); // Guardar nuevo HighScore
        }
        else
        {
            int difference = highScore - score;
            if (difference <= 5)
                gameOverMessage.text = "¡Casi logras un récord! 🔥";
            else if (difference <= 15)
                gameOverMessage.text = "¡Buen intento! Sigue mejorando. 💪";
            else
                gameOverMessage.text = "¡Puedes hacerlo mejor! 🚀";
        }

        highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void WatchAdToContinue()
    {
        Debug.Log("📢 Mostrando anuncio para continuar...");

        // Aquí va la lógica de integración con anuncios (Google AdMob o Unity Ads)
        ContinueGame();
    }

    public void ContinueGame()
    {
        Debug.Log("🎮 Continuando la partida tras ver el anuncio");
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
        gameStarted = true;
        currentLives = 1;
        gameMusic.Play();
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
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void HitPause(float duration)
    {
        StartCoroutine(Pause(duration));
    }

    private IEnumerator Pause(float duration)
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
    }
}
