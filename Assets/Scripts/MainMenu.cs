using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("ColorDash_Main"); // Asegúrate de que tu escena de juego se llame así
    }
}
