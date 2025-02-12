using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 8f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool isGrounded = false;

    private Color[] colors = {
        new Color32(255, 255, 0, 255),
        new Color32(255, 138, 145, 255),
        new Color32(11, 82, 174, 255),
        new Color32(0, 255, 0, 255)
    };

    private int currentColorIndex = 0;
    private float lastDirection = 1f; // Guarda la última dirección del jugador

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        sr.color = colors[currentColorIndex];
    }

    public void MoveLeft()
    {
        rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
        FlipSprite(0);
    }

    public void MoveRight()
    {
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
        FlipSprite(180);
    }

    public void StopMovement()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); // Detener el movimiento al soltar la pantalla
    }

    void FlipSprite(float yRotation)
    {
        if (yRotation != lastDirection)
        {
            transform.rotation = Quaternion.Euler(0, yRotation, 0);
            lastDirection = yRotation;
        }
    }

    public void ChangeColorForward()
    {
        currentColorIndex = (currentColorIndex + 1) % colors.Length;
        sr.color = colors[currentColorIndex];
    }

    public void ChangeColorBackward()
    {
        currentColorIndex = (currentColorIndex - 1 + colors.Length) % colors.Length;
        sr.color = colors[currentColorIndex];
    }

    public void Jump()
    {
        if (isGrounded) // Solo permite saltar si está en el suelo
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
