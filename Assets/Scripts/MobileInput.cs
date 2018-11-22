using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : MonoBehaviour 
{
    private const float DEADZONE = 100.0f;
    public static MobileInput Instance {set; get; }

    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private Vector2 swipeDelta, starTouch;

    public bool Tap{ get { return tap; }}
    public Vector2 SwipeDelta{ get { return swipeDelta; }}
    public bool SwipeLeft { get { return swipeLeft; }}
    public bool SwipeDown { get { return swipeDown; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeRight { get { return swipeRight; } }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        //restea boleanos
        tap = swipeLeft = swipeUp = swipeDown = swipeRight = false;

        //Lets check for inputs

        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            starTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            starTouch = swipeDelta = Vector2.zero;
        }
        #endregion

        #region Mobile Inputs
        if (Input.touches.Length != 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                starTouch = Input.mousePosition;
            }

            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                starTouch = swipeDelta = Vector2.zero;

            }
        }
        #endregion

        //Calculate Distance

        swipeDelta = Vector2.zero;
        if (starTouch != Vector2.zero)
        {
            //lets check wit mobile
            if(Input.touches.Length != 0)
            {
                swipeDelta = Input.touches[0].position - starTouch;
            }

            //Lets check whit standalone
            else if(Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - starTouch;
            }
        }

        //check  where beyond the deadzone
        if (swipeDelta.magnitude > DEADZONE)
        {
            //This is a confirmed swipe
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x)>Mathf.Abs(y)){

                //left or right
                if (x < 0)
                    swipeLeft = true;
                else
                    swipeRight = true;
            }
            else
            {
                //Up or Down
                if (y < 0)
                    swipeDown = true;
                else
                    swipeUp = true;

            }

            starTouch = swipeDelta = Vector2.zero;
        }

    }

}
