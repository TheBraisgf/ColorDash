using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    private float backButtonPressedTime = 0f;
    private float doublePressTime = 0.5f; // Tiempo en segundos para detectar doble toque

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                HandleBackButton();
            }
        }
    }

    void HandleBackButton()
    {
        if (Time.time - backButtonPressedTime < doublePressTime)
        {
            Debug.Log("🔙 Doble toque detectado. Volviendo al menú...");
            SceneManager.LoadScene("MenuScene"); // Asegúrate de reemplazarlo con el nombre correcto
        }
        else
        {
            backButtonPressedTime = Time.time;
            Debug.Log("🔙 Primer toque detectado. Presiona otra vez para salir.");
        }
    }
}
