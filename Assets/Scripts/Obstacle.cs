using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private SpriteRenderer sr;

    public enum ObstacleType { yellow_spark, red_stone, blue_water, green_leaf }
    public ObstacleType type; // Seleccionar el tipo de obstáculo en Unity

    private Color assignedColor;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        AssignColor();
    }

    void AssignColor()
    {
        switch (type)
        {
            case ObstacleType.yellow_spark:
                assignedColor = new Color32(255, 255, 0, 255); // Amarillo
                break;
            case ObstacleType.red_stone:
                assignedColor = new Color32(255, 138, 145, 255); // Rojo
                break;
            case ObstacleType.blue_water:
                assignedColor = new Color32(11, 82, 174, 255); // Azul
                break;
            case ObstacleType.green_leaf:
                assignedColor = new Color32(0, 255, 0, 255); // Verde
                break;
        }
        sr.color = assignedColor;
    }

    private void OnTriggerEnter2D(Collider2D other)
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
