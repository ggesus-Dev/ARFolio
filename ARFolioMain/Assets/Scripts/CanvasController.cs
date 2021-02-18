using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {
    public Vector2 activeBetweenStartPoint, activeBetweenEndPoint;
    private bool _selectionOpen = false;
    private GameObject _mySelectionView;
    private GameObject _debugText;
    public  LeanTweenType _easeType;

    private void Awake() {
        _debugText = transform.GetChild(0).gameObject;
        SwipeController.onSwipe += swipeControllerOnSwipe;
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)) {
            selectionController();
        }
    }

    private void swipeControllerOnSwipe(SwipeController.SwipeData data) {
        switch(data.direction) {
            case SwipeController.SwipeDirection.UP: 
                if(data.startPosition.y < activeBetweenEndPoint.y) {
                    selectionController();
                    _debugText.GetComponent<Text>().text += "\nUP";
                }
                break;
            case SwipeController.SwipeDirection.DOWN: 
                if(data.startPosition.y > activeBetweenEndPoint.y) {
                    selectionController();
                    _debugText.GetComponent<Text>().text += "\nDOWN";
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
            LeanTween.moveY(_mySelectionView, 150f, 0.2f).setEase(_easeType);                                               // tween animation
            _selectionOpen = true;
        } else {                                                                                                            // if already open
            LeanTween.moveY(_mySelectionView, -200f, 0.1f).setOnComplete(onComplete, _mySelectionView).setEase(_easeType);
            _selectionOpen = false;
        }
    }
}
