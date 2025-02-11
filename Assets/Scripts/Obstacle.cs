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
        if (other.CompareTag("Player"))
        {
            SpriteRenderer playerSr = other.GetComponent<SpriteRenderer>();

            if (playerSr != null)
            {
                if (playerSr.color != sr.color)
                {
                    Debug.Log("❌ ¡Game Over! Los colores no coinciden.");
                    GameManager.Instance.GameOver();
                    Destroy(other.gameObject); // Eliminar al jugador
                }
                else
                {
                    Debug.Log("✅ ¡Colisión correcta! +1 Punto");
                    GameManager.Instance.AddPoint();
                    Destroy(gameObject);
                }
            }
        }
    }

}
