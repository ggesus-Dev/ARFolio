using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour {
    public Vector2 activeBetweenStartPoint, activeBetweenEndPoint;

    private void Awake() {
        SwipeController.onSwipe += swipeControllerOnSwipe;
    }

    private void swipeControllerOnSwipe(SwipeController.SwipeData data) {
        switch(data.direction) {
            case SwipeController.SwipeDirection.UP: 
                if(data.startPosition.y < activeBetweenEndPoint.y) {
                    GameObject _selectionView = gameObject.transform.GetChild(0).gameObject;
                    _selectionView.SetActive(true);
                }
                break;
            case SwipeController.SwipeDirection.DOWN: 
                break;
            case SwipeController.SwipeDirection.LEFT: 
                break;
            case SwipeController.SwipeDirection.RIGHT: 
                break;
        }

        
        if(data.direction == SwipeController.SwipeDirection.UP) {
            
        }
    }

    private void onComplete() {
        Destroy(gameObject);
        
    }
}
