using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour {
    public Vector2 activeBetweenStartPoint, activeBetweenEndPoint;
    private bool _selectionOpen = false;
    private GameObject _mySelectionView;

    private void Awake() {
        SwipeController.onSwipe += swipeControllerOnSwipe;
    }

    private void swipeControllerOnSwipe(SwipeController.SwipeData data) {
        switch(data.direction) {
            case SwipeController.SwipeDirection.UP: 
                if(data.startPosition.y < activeBetweenEndPoint.y) {
                    selectionController();
                }
                break;
            case SwipeController.SwipeDirection.DOWN: 
                if(data.startPosition.y > activeBetweenEndPoint.y) {
                    selectionController();
                }
                break;
            case SwipeController.SwipeDirection.LEFT: 
                break;
            case SwipeController.SwipeDirection.RIGHT: 
                break;
        }
    }

    private static void onComplete(object toDestroy) {
        Destroy((GameObject)toDestroy);
    }

    private void selectionController() { 
        if(!_selectionOpen) {
            GameObject _viewportSelection = (GameObject)Resources.Load("Prefabs/ViewportSelection");                        // get prefab reference
            _mySelectionView = Instantiate(_viewportSelection, gameObject.transform, false);                                // create and make its parent the canvas
            LeanTween.moveY(gameObject, 150f, 0.1f);                                                                        // tween animation
            _selectionOpen = true;
        } else {                                                                                                            // if already open
            LeanTween.moveY(_mySelectionView, 0f, 0.1f).setOnComplete(onComplete, _mySelectionView);
            _selectionOpen = false;
        }
    }
}
