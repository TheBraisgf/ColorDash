using UnityEngine;

public class LlamaAnimation : MonoBehaviour
{
    public float moveSpeedMin = 2f;  // Velocidad mínima de movimiento
    public float moveSpeedMax = 5f;  // Velocidad máxima de movimiento
    public float jumpForce = 5f;  // Fuerza del salto
    public float jumpIntervalMin = 1f; // ⏳ Salto más frecuente
    public float jumpIntervalMax = 3f;
    public float stuckTimeThreshold = 2.5f; // 🛑 Tiempo máximo sin moverse antes de forzar un salto

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private float nextJumpTime;
    private float moveSpeed;
    private int direction = 1;  // 1 = Derecha, -1 = Izquierda
    private float lastMoveTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        moveSpeed = Random.Range(moveSpeedMin, moveSpeedMax);
        direction = Random.value > 0.5f ? 1 : -1;
        SetNextJumpTime();
        lastMoveTime = Time.time;
        UpdateSpriteDirection();
    }

    void Update()
    {
        Move();
        HandleJump();
        HandleStuckSituation();
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
        UpdateSpriteDirection();
    }

    void HandleJump()
    {
        if (Time.time >= nextJumpTime && rb.linearVelocity.y == 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            SetNextJumpTime();
        }
    }

    void HandleStuckSituation()
    {
        // Si ha estado parado demasiado tiempo, salta y cambia de dirección
        if (Time.time - lastMoveTime >= stuckTimeThreshold)
        {
            direction *= -1; // Cambia la dirección
            rb.linearVelocity = new Vector2(direction * moveSpeed, jumpForce * 1.5f); // Salta con más fuerza
            SetNextJumpTime();
            lastMoveTime = Time.time; // Reinicia el contador
        }

        // Si la llama está en movimiento, actualiza el tiempo de la última vez que se movió
        if (rb.linearVelocity.x != 0)
        {
            lastMoveTime = Time.time;
        }
    }

    void SetNextJumpTime()
    {
        nextJumpTime = Time.time + Random.Range(jumpIntervalMin, jumpIntervalMax);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Si choca con otra llama, cambia de dirección y salta un poco
        if (collision.gameObject.CompareTag("Llama"))
        {
            direction = Random.value > 0.5f ? 1 : -1;
            moveSpeed = Random.Range(moveSpeedMin, moveSpeedMax);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce / 1.5f); // Rebota levemente
        }
        else if (collision.gameObject.CompareTag("Pared"))
        {
            direction *= -1; // Rebota en dirección opuesta
        }
        else
        {
            // Si choca con cualquier otro objeto, rebotar aleatoriamente
            direction = Random.value > 0.5f ? 1 : -1;
        }

        UpdateSpriteDirection();
    }

    void UpdateSpriteDirection()
    {
        sr.flipX = direction > 0 ? true : false;
    }
}
