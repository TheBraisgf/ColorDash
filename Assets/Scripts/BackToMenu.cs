using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    private float backButtonPressedTime = 0f;
    private float doublePressTime = 0.5f; // Tiempo en segundos para detectar doble toque

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Detecta el botón de atrás en Android
        {
            if (Time.time - backButtonPressedTime < doublePressTime)
            {
                // Si el usuario presiona dos veces en menos de 0.5s, vuelve al menú
                Debug.Log("🔙 Doble toque detectado. Volviendo al menú...");
                SceneManager.LoadScene("MenuScene"); // Reemplaza "MenuScene" con el nombre real de tu escena de menú
            }
            else
            {
                // Primer toque, guarda el tiempo
                backButtonPressedTime = Time.time;
                Debug.Log("🔙 Primer toque detectado. Presiona otra vez para salir.");
            }
        }
    }
}
