using UnityEngine;

public class LlamaAnimation : MonoBehaviour
{
    public float moveSpeedMin = 2f;
    public float moveSpeedMax = 5f;
    public float jumpForce = 5f;
    public float jumpIntervalMin = 1f;
    public float jumpIntervalMax = 3f;
    public float stuckTimeThreshold = 2.5f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private float nextJumpTime;
    private float moveSpeed;
    private int direction = 1;
    private float lastMoveTime;

    public AudioSource jumpSound; // 🎵 Sonido de salto
    private float lastJumpSoundTime = 0f; // Para evitar spam de sonido
    private float jumpSoundCooldown = 0.3f; // Evitar que suene demasiado seguido

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
            PlayJumpSound(); // 🔊 Sonido de salto
            SetNextJumpTime();
        }
    }

    void HandleStuckSituation()
    {
        if (Time.time - lastMoveTime >= stuckTimeThreshold)
        {
            direction *= -1;
            rb.linearVelocity = new Vector2(direction * moveSpeed, jumpForce * 1.5f);
            PlayJumpSound(); // 🔊 Sonido de rebote si estuvo atascado
            SetNextJumpTime();
            lastMoveTime = Time.time;
        }

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
        if (collision.gameObject.CompareTag("Llama"))
        {
            direction = Random.value > 0.5f ? 1 : -1;
            moveSpeed = Random.Range(moveSpeedMin, moveSpeedMax);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce / 1.5f);
            PlayJumpSound(); // 🔊 Sonido al chocar con otra llama
        }
        else if (collision.gameObject.CompareTag("Pared"))
        {
            direction *= -1;
            PlayJumpSound(); // 🔊 Sonido al rebotar en la pared
        }
        else
        {
            direction = Random.value > 0.5f ? 1 : -1;
        }

        UpdateSpriteDirection();
    }

    void UpdateSpriteDirection()
    {
        sr.flipX = direction > 0 ? true : false;
    }

    void PlayJumpSound()
    {
        if (jumpSound != null && Time.time - lastJumpSoundTime > jumpSoundCooldown)
        {
            jumpSound.pitch = Random.Range(0.9f, 1.1f); // Variación de tono 🔊
            jumpSound.Play();
            lastJumpSoundTime = Time.time; // Evita spam de sonido
        }
    }
}
