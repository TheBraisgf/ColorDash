using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private SpriteRenderer sr;

    public enum ObstacleType { yellow_spark, red_stone, blue_water, green_leaf }
    public ObstacleType type; // Seleccionar el tipo de obstáculo en Unity

    private int assignedColorIndex; // Índice del color en lugar de color directo

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
                assignedColorIndex = 0;
                break;
            case ObstacleType.red_stone:
                assignedColorIndex = 1;
                break;
            case ObstacleType.blue_water:
                assignedColorIndex = 2;
                break;
            case ObstacleType.green_leaf:
                assignedColorIndex = 3;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                if (player.GetCurrentColorIndex() != assignedColorIndex)
                {
                    Debug.Log("❌ ¡Game Over! Los colores no coinciden.");
                    GameManager.Instance.HitPause(0.2f); // 🔥 Pequeña pausa de impacto
                    GameManager.Instance.TakeDamage();
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

