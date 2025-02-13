using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f; // Aumentamos la velocidad para un tilt más sensible
    public float jumpForce = 8f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool isGrounded = false;

    public AudioSource jumpSound;
    public ParticleSystem jumpParticles;
    public ParticleSystem colorChangeParticles;

    public Sprite[] flameSprites;
    private int currentColorIndex = 0;
    private float lastDirection = 1f;
    private float colorChangeTimer;
    private float colorChangeInterval;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = flameSprites[currentColorIndex];

        SetRandomColorChangeTime();
    }

    void Update()
    {
        HandleAutoColorChange();
    }

    public void MoveWithTilt(float tilt)
    {
        float tiltSensitivity = 2.0f; // Ajustado para menor velocidad
        float smoothFactor = 0.1f; // Suaviza la transición del movimiento

        float targetVelocityX = tilt * speed * tiltSensitivity;
        rb.linearVelocity = new Vector2(Mathf.Lerp(rb.linearVelocity.x, targetVelocityX, smoothFactor), rb.linearVelocity.y);

        if (tilt > 0.05f) FlipSprite(180);
        else if (tilt < -0.05f) FlipSprite(0);
    }


    public void StopMovement()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    void FlipSprite(float yRotation)
    {
        if (yRotation != lastDirection)
        {
            transform.rotation = Quaternion.Euler(0, yRotation, 0);
            lastDirection = yRotation;
        }
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;

            if (jumpSound != null)
                jumpSound.Play();

            if (jumpParticles != null)
            {
                jumpParticles.Stop();
                jumpParticles.transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);

                var main = jumpParticles.main;
                main.startColor = sr.color;

                jumpParticles.Play();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void HandleAutoColorChange()
    {
        colorChangeTimer += Time.deltaTime;
        if (colorChangeTimer >= colorChangeInterval)
        {
            ChangeSpriteRandomly();
            SetRandomColorChangeTime();
        }
    }

    void SetRandomColorChangeTime()
    {
        colorChangeTimer = 0f;
        colorChangeInterval = Random.Range(3f, 5f);
    }

    public void ChangeSpriteRandomly()
    {
        currentColorIndex = Random.Range(0, flameSprites.Length);
        sr.sprite = flameSprites[currentColorIndex];

        if (colorChangeParticles != null)
        {
            colorChangeParticles.Stop();
            colorChangeParticles.Play();
        }
    }

    public int GetCurrentColorIndex()
    {
        return currentColorIndex;
    }



}
