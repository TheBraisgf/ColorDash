using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SplashScreen : MonoBehaviour
{
    public Image logo;
    public float fadeDuration = 1.5f;

    void Start()
    {
        StartCoroutine(FadeInAndLoad());
    }

    IEnumerator FadeInAndLoad()
    {
        float elapsedTime = 0;
        Color color = logo.color;

        // Fade In
        while (elapsedTime < fadeDuration)
        {
            color.a = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            logo.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1f); // Mantiene el logo por un momento

        // Fade Out
        elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            color.a = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            logo.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene("MainMenu"); // Cambia a la escena del menú
    }
}
