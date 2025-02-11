using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private SpriteRenderer sr;
    private Color[] colors = { Color.red, Color.green, Color.blue, Color.yellow }; // Colores disponibles
    private Color assignedColor; // Color asignado al obstáculo

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        AssignRandomColor();
    }

    void AssignRandomColor()
    {
        int randomIndex = Random.Range(0, colors.Length);
        assignedColor = colors[randomIndex];
        sr.color = assignedColor;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Colisión detectada con: {other.name}");

        if (other.CompareTag("Player"))
        {
            SpriteRenderer playerSr = other.GetComponent<SpriteRenderer>();

            if (playerSr != null)
            {
                Debug.Log($"Color del Jugador: {playerSr.color}, Color del Obstáculo: {assignedColor}");

                if (playerSr.color != assignedColor)
                {
                    Debug.Log("❌ ¡Game Over! Los colores no coinciden.");
                    Time.timeScale = 0; // Detener el juego
                    Destroy(other.gameObject); // Eliminar al jugador
                }
                else
                {
                    Debug.Log("✅ ¡Colisión correcta! Sigues en juego.");
                    Destroy(gameObject); // Eliminar el obstáculo atravesado correctamente
                }
            }
        }
    }
}
