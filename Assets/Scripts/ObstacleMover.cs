using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public float speed = 5f; // Velocidad de caída del obstáculo

    void Update()
    {
        // Mueve el obstáculo hacia abajo
        transform.position += Vector3.down * speed * Time.deltaTime;

        // Si el obstáculo sale de la pantalla por la parte inferior, se destruye
        if (transform.position.y < -6f) // Ajusta el valor según la pantalla
        {
            Destroy(gameObject);
        }
    }
}
