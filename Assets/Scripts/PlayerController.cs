using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private Color[] colors = { Color.red, Color.green, Color.blue, Color.yellow };
    private int currentColorIndex = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        sr.color = colors[currentColorIndex];
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeColor();
        }
    }

    void ChangeColor()
    {
        currentColorIndex = (currentColorIndex + 1) % colors.Length;
        sr.color = colors[currentColorIndex];
    }
}
