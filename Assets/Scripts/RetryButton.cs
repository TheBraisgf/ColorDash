using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections.Generic; // Importante para List<>

public class RetryButton : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Clic en PC
        {
            CheckUIClick(Input.mousePosition);
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) // Toque en móvil
        {
            CheckUIClick(Input.GetTouch(0).position);
        }
    }

    void CheckUIClick(Vector2 screenPosition)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = screenPosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject == gameObject) // ✅ Si el objeto clicado es el RetryButton
            {
                RestartGame();
                return;
            }
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
