using UnityEngine;
using UnityEngine.UI;

public class ControllerCanvas : MonoBehaviour {
    public  Vector2         screenRes;
    public  LeanTweenType   easeType;
    private GameObject      _mySelectionView = null;
    private GameObject      _debugText;

    private void Awake() {
        screenRes = new Vector2(Screen.width, Screen.height);                       // get screen information
        _debugText = transform.GetChild(0).gameObject;                              // get reference to debug text
        ControllerSwipe.OnSwipe += OnSwipeController;
    }

    // Mouse Click input option if in editor
    # if UNITY_EDITOR
    private void Update() {
        if(Input.GetMouseButtonDown(0)) 
            HandleMousePosition();
    }
   
    private void HandleMousePosition() {
        if(_mySelectionView == null && Input.mousePosition.y < screenRes.y / 3) {
            SelectionController(true); 
            return;            
        }
        SelectionController(false);
    }
    # endif

    private void OnSwipeController(ControllerSwipe.SwipeData data) {
        switch(data.direction) {
            case ControllerSwipe.SwipeDirection.UP:                                 // if direction is up
                SwipeUp(data.startPosition);
                break;
            case ControllerSwipe.SwipeDirection.DOWN: 
                SwipeDown(data.startPosition);
                break;
            case ControllerSwipe.SwipeDirection.LEFT: 
                break;
            case ControllerSwipe.SwipeDirection.RIGHT: 
                break;
        }
    }

    private void SwipeUp(Vector2 startPos) {
        if(startPos.y < screenRes.y / 3) {                                          // start to swipe up within the lower third of the screen 
            SelectionController(true);
            _debugText.GetComponent<Text>().text += "\nSWIPE UP";
        }
    }

    private void SwipeDown(Vector2 startPos) {
        if(startPos.y > screenRes.y / 3) {                                          // start to swipe within top 2 thirds
            SelectionController(false);
            _debugText.GetComponent<Text>().text += "\nDOWN";
        }
    }

    private static void OnAnimationComplete(object toDestroy) {
        Destroy((GameObject)toDestroy);
    }

    private void SelectionController(bool open) { 
        if(open && _mySelectionView == null) {
            GameObject _viewportSelection = (GameObject)Resources.Load("Prefabs/ViewportSelection");    // load viewport prefab
            _mySelectionView = Instantiate(_viewportSelection, gameObject.transform, false);            // create and make its parent the canvas
            LeanTween.moveY(_mySelectionView, 150f, 0.2f).setEase(easeType);                            // tween animation
            return;
        } 
        
        if(!open && _mySelectionView != null) {                                                         // if already open
            LeanTween.moveY(_mySelectionView, -200f, 0.1f).setEase(easeType).setOnComplete(OnAnimationComplete, _mySelectionView);
            _mySelectionView = null;
        }
    }
}
