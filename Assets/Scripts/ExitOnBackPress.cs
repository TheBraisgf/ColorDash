using UnityEngine;

public class ExitOnBackPress : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Detecta el botón de atrás en Android
        {
            Application.Quit(); // Cierra la aplicación en Android
        }
    }
}
