using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ControllerCanvas : MonoBehaviour {
    public  Vector2         screenRes;
    public  LeanTweenType   easeType;
    private GameObject      _mySelectionView = null;
    private GameObject      _debugText;

    private void Awake() {
        screenRes = new Vector2(Screen.width, Screen.height);                       // get screen information
        _debugText = transform.GetChild(0).gameObject;                              // get reference to debug text
        ControllerSwipe.onSwipe += swipeControllerOnSwipe;
    }

    // Mouse Click input option if in editor
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
            case ControllerSwipe.SwipeDirection.UP:                                 // if direction is up
                // start to swipe up within the lower third of the screen 
                if(data.startPosition.y < screenRes.y / 3) {
                    selectionController(true);
                    _debugText.GetComponent<Text>().text += "\nSWIPE UP";
                }
                break;
            case ControllerSwipe.SwipeDirection.DOWN: 
                // start to swipe within top 2 thinds
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
            LeanTween.moveY(_mySelectionView, 150f, 0.2f).setEase(easeType);                                               // tween animation
        } else if(!open && _mySelectionView != null) {                                                                                                            // if already open
            LeanTween.moveY(_mySelectionView, -200f, 0.1f).setEase(easeType).setOnComplete(onComplete, _mySelectionView);
            _mySelectionView = null;
        }
    }
}
