using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public float fallSpeed = 5f; // Velocidad inicial
    private float difficultyMultiplier = 0.05f; // Cuánto aumenta la velocidad con el tiempo

    void Update()
    {
        // Aumenta la velocidad con el tiempo
        fallSpeed += difficultyMultiplier * Time.deltaTime;

        // Mueve el obstáculo hacia abajo
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        // Si el obstáculo sale de la pantalla por abajo, se destruye
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }
}
