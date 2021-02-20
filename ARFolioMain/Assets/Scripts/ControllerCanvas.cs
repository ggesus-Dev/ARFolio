using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerCanvas : MonoBehaviour {
    public Vector2 screenRes;
    private GameObject _mySelectionView = null;
    private GameObject _debugText;
    public  LeanTweenType _easeType;

    private void Awake() {
        screenRes = new Vector2(Screen.width, Screen.height);
        _debugText = transform.GetChild(0).gameObject;
        ControllerSwipe.onSwipe += swipeControllerOnSwipe;
    }

    # if UNITY_EDITOR
    private void Update() {
        if(Input.GetMouseButtonDown(0)) {
            if(_mySelectionView == null && Input.mousePosition.y < screenRes.y / 3) 
                selectionController(true); 
            else if(_mySelectionView != null && Input.mousePosition.y > screenRes.y / 3) 
                selectionController(false);
        }
    }
    # endif

    private void swipeControllerOnSwipe(ControllerSwipe.SwipeData data) {
        switch(data.direction) {
            case ControllerSwipe.SwipeDirection.UP: 
                // start to swipe up within the lower third of the screen 
                if(data.startPosition.y < screenRes.y / 3) {
                    selectionController(true);
                    _debugText.GetComponent<Text>().text += "\nSWIPE UP";
                }
                break;
            case ControllerSwipe.SwipeDirection.DOWN: 
                // start to swipe above 
                if(data.startPosition.y > screenRes.y / 3) {
                    selectionController(false);
                    _debugText.GetComponent<Text>().text += "\nDOWN";
                }
                break;
            case ControllerSwipe.SwipeDirection.LEFT: 
                break;
            case ControllerSwipe.SwipeDirection.RIGHT: 
                break;
        }
    }

    private static void onComplete(object toDestroy) {
        Destroy((GameObject)toDestroy);
    }

    private void selectionController(bool open) { 
        if(open && _mySelectionView == null) {
            GameObject _viewportSelection = (GameObject)Resources.Load("Prefabs/ViewportSelection");                        // get prefab reference
            _mySelectionView = Instantiate(_viewportSelection, gameObject.transform, false);                                // create and make its parent the canvas
            LeanTween.moveY(_mySelectionView, 150f, 0.2f).setEase(_easeType);                                               // tween animation
        } else if(!open && _mySelectionView != null) {                                                                                                            // if already open
            LeanTween.moveY(_mySelectionView, -200f, 0.1f).setEase(_easeType).setOnComplete(onComplete, _mySelectionView);
            _mySelectionView = null;
        }
    }
}
