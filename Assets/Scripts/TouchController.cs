using UnityEngine;

public class TouchController : MonoBehaviour
{
    public PlayerController player;

    private float colorChangeTimer;
    private float colorChangeInterval;

    void Start()
    {
        SetRandomColorChangeTime();
    }

    void Update()
    {
        HandleTiltMovement();
        HandleTouchInput();
        HandleAutoColorChange();
    }

    void HandleTiltMovement()
    {
        float tilt = Input.acceleration.x;
        player.MoveWithTilt(tilt);
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            player.Jump(); // Ahora el salto se activa con solo tocar la pantalla
        }
    }

    void HandleAutoColorChange()
    {
        colorChangeTimer += Time.deltaTime;
        if (colorChangeTimer >= colorChangeInterval)
        {
            player.ChangeSpriteRandomly();
            SetRandomColorChangeTime();
        }
    }

    void SetRandomColorChangeTime()
    {
        colorChangeTimer = 0f;
        colorChangeInterval = Random.Range(3f, 5f);
    }
}
