using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SwipeDirection
{
    Up,
    Down,
    Left,
    Right
}

public class SwipeDetector : MonoBehaviour
{
    [SerializeField] SwipeDirection swipeDirection;
    public GameEvent swipeEvent;

    [Range(0,200)]
    public float sensitivity;

    private Vector2 startTouch, swipeDelta;
    private bool isDraging;

    private void Update()
    {

        #region StandaloneInput
        if (Input.GetMouseButtonDown(0))
        {
            isDraging = true;
            startTouch = Input.mousePosition; // Tap
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDraging = false;
            Reset();
        }
        #endregion

        #region MobileInput
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                isDraging = true;
                startTouch = Input.touches[0].position; // Tap
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDraging = false;
                Reset();
            }
        }
        #endregion

        // Calculate the distanse
        swipeDelta = Vector2.zero;
        if (isDraging)
        {
            if (Input.touches.Length > 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }
            else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
        }

        // DId we cross the deadzone?
        if (swipeDelta.magnitude > sensitivity)
        {
            // Which direction?
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                //Left or right
                if (x < 0 && swipeDirection == SwipeDirection.Left)
                {
                    swipeEvent.Raise();
                }
                else if(x > 0 && swipeDirection == SwipeDirection.Right)
                {
                    swipeEvent.Raise();
                }
            }
            else
            {
                // Up or down
                if (y < 0 && swipeDirection == SwipeDirection.Down)
                {
                    swipeEvent.Raise();
                }
                else if (y > 0 && swipeDirection == SwipeDirection.Up)
                {
                    swipeEvent.Raise();
                }
            }
            Reset();
        }

    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
    }
}
