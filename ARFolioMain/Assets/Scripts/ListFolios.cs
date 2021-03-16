using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListFolios : MonoBehaviour {
    private List<string>        _folioList = new List<string>();
    private List<GameObject>    _optBtnList = new List<GameObject>();
    private Text                _debugText;
    
    void Start() {
        var _controller = transform.parent.transform.parent;
        _controller.GetComponent<ControllerGeneral>().CheckFolios();
        _folioList = _controller.GetComponent<ControllerGeneral>().folioList;
        DrawButtons();
    }

    // Draw buttons for each found folio
    void DrawButtons() {
        GameObject _btnPrefab = (GameObject)Resources.Load("Prefabs/BtnFolioOption");
        Transform _parent = gameObject.transform.GetChild(0).GetChild(0);
        _optBtnList.Clear();                                                                        // if need to redraw

        for(int i = 0; i < _folioList.Count; i++) {
            _optBtnList.Add(Instantiate(_btnPrefab, Vector3.zero, Quaternion.identity, _parent));   // Vector3.zero because content component will adjust positions
            _optBtnList[i].GetComponentInChildren<Text>().text = _folioList[i];                     // change text to name
        }
    }
}
