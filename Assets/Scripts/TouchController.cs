using UnityEngine;

public class TouchController : MonoBehaviour
{
    public PlayerController player;
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    private float minSwipeDistance = 100f; // Distancia mínima para considerar un swipe
    private bool isSwipeDetected = false; // Nueva variable para evitar toques accidentales

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position;
                    endTouchPosition = touch.position;
                    isSwipeDetected = false; // Reiniciamos el swipe detectado
                    break;

                case TouchPhase.Moved:
                    endTouchPosition = touch.position;
                    break;

                case TouchPhase.Stationary:
                    if (!isSwipeDetected) // Solo mueve si NO ha sido un swipe
                        DetectTap(touch.position);
                    break;

                case TouchPhase.Ended:
                    DetectTouchOrSwipe();
                    if (!isSwipeDetected) // Solo mueve si NO ha sido un swipe
                        DetectTap(touch.position);

                    player.StopMovement(); // Detener movimiento al soltar la pantalla
                    break;
            }
        }
    }

    void DetectTouchOrSwipe()
    {
        Vector2 swipeDelta = endTouchPosition - startTouchPosition;
        float swipeDistance = swipeDelta.magnitude;

        if (swipeDistance > minSwipeDistance)
        {
            isSwipeDetected = true; // Marcamos que fue un swipe y no un toque

            // Determinar la dirección del swipe
            swipeDelta.Normalize();

            if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
            {
                // Swipe horizontal (cambio de color)
                if (swipeDelta.x > 0)
                    player.ChangeColorForward();
                else
                    player.ChangeColorBackward();
            }
            else
            {
                // Swipe vertical (salto)
                if (swipeDelta.y > 0)
                {
                    player.Jump();
                    isSwipeDetected = true; // Evita cualquier otro movimiento tras el swipe
                }
            }
        }
    }

    void DetectTap(Vector2 position)
    {
        if (isSwipeDetected) return; // Si ya detectó un swipe, no mueve

        if (position.x < Screen.width / 2)
            player.MoveLeft();
        else
            player.MoveRight();
    }
}
