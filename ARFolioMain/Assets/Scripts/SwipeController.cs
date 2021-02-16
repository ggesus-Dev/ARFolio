using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SwipeController : MonoBehaviour {
    [SerializeField]
    private float _minDistance = 100f;
    [SerializeField]
    private Vector2 _startPos, _endPos;

    public bool _detectOnRelease = true; 

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

    public static event Action<SwipeData> onSwipe = delegate{};     // won't return a value

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
                    break;
            }

            float _horDistance = Mathf.Abs(_startPos.x - _endPos.y);
            float _verDistance = Mathf.Abs(_startPos.y - _endPos.y);
            if(checkSwipe(_horDistance, _verDistance)) {
                SwipeDirection _dir;                    // direction user swiped in
            
                if(_verDistance > _horDistance) {       // if it was a vertical swipe
                    Debug.Log("Vertical Swipe");
                    
                    if(_endPos.y > _startPos.y) 
                        _dir = SwipeDirection.UP; else _dir = SwipeDirection.DOWN;      // if our end position is greater than our start, direction is up
                } else {
                    Debug.Log("Horizontal Swipe");

                    if(_endPos.x > _startPos.x)
                        _dir = SwipeDirection.LEFT; else _dir = SwipeDirection.RIGHT;
                }

                sendSwipe(_dir);
            } else {
                Debug.Log("Tapped screen");
            }
        }
    }

    private bool checkSwipe(float horDistance, float verDistance) {
        if(verDistance > _minDistance || horDistance > _minDistance) return true;       // swiped
        return false;                                                                   // treat as tap
    }

    private void sendSwipe(SwipeDirection dir) {
        SwipeData _swipeData = new SwipeData() {
            direction = dir,
            startPosition = _startPos,
            endPosition = _endPos
        };

        onSwipe(_swipeData);
    }
}
