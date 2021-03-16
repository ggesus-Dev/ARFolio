using System;
using UnityEngine;

public class ControllerSwipe : MonoBehaviour {
    [SerializeField]
    private float   _minDistance = 300f;
    [SerializeField]
    private Vector2 _startPos, _endPos;
    private bool    _detectOnRelease = true; 

    // thanks to Jason Weimann and whoever he took parts from for SendSwipe function
    public struct SwipeData {
        public Vector2 startPosition;
        public Vector2 endPosition;
        public SwipeDirection direction;
    }

    public enum SwipeDirection {
        UP, 
        DOWN, 
        LEFT,
        RIGHT
    };

    public static event Action<SwipeData> OnSwipe = delegate{};     // won't return a value

    private void Update() {
        if(Input.touchCount > 0) {
            Touch _touch = Input.GetTouch(0);

            switch(_touch.phase) {
                case TouchPhase.Began:  
                    _startPos = _touch.position;
                    break;
                case TouchPhase.Moved:
                    if(_detectOnRelease) break;
                    _endPos = _touch.position - _startPos;   
                    break;
                case TouchPhase.Ended:
                    _endPos = _touch.position;
                    GetSwipeData();
                    break;
            }
        }
    }

    private void GetSwipeData() {
        float _horDistance = Mathf.Abs(_startPos.x - _endPos.x);
        float _verDistance = Mathf.Abs(_startPos.y - _endPos.y);

        if(CheckSwipe(_horDistance, _verDistance)) {
            SwipeDirection _dir = CheckSwipeDirection(_horDistance, _verDistance);
            SendSwipe(_dir);
        }
    }

    private bool CheckSwipe(float horDistance, float verDistance) {
        return (verDistance > _minDistance || horDistance > _minDistance) ? true : false;   // Check to see if it was a respectable swipe or just a leisurely tap of the screen
    }

    private SwipeDirection CheckSwipeDirection(float horDistance, float verDistance) {
        if(verDistance > horDistance) {                                                     // if it was a vertical swipe
            Debug.Log("Vertical Swipe");
            return GetVerticalDirection();                                                  // either UP or DOWN
        }

        Debug.Log("Horizontal Swipe");
        return GetHorizontalDirection();                                                    // either LEFT or RIGHT
    }

    private SwipeDirection GetVerticalDirection() {
        return (_endPos.y > _startPos.y) ? SwipeDirection.UP : SwipeDirection.DOWN;         // if our end position is greater than our start, direction is up
    }
    
    private SwipeDirection GetHorizontalDirection() {
        return (_endPos.x > _startPos.x) ? SwipeDirection.LEFT : SwipeDirection.RIGHT;      // if our end position is greater than our start, direction is up
    }

    private void SendSwipe(SwipeDirection dir) {
        SwipeData _swipeData = new SwipeData() {
            direction = dir,
            startPosition = _startPos,
            endPosition = _endPos
        };

        OnSwipe(_swipeData);
    }
}
