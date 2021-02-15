using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionOpen : MonoBehaviour {
    public Vector2 activeBetweenStartPoint, activeBetweenEndPoint;

    private void Awake() {
        SwipeController.onSwipe += swipeControllerOnSwipe;
    }

    private void swipeControllerOnSwipe(SwipeController.SwipeData data) {
        if(data.direction == SwipeController.SwipeDirection.UP) {
            Debug.Log("Test");
        }
    }
}
