using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ListFolios : MonoBehaviour {
    public  AssetBundle         folioActive;
    public  string[]            folioAssets;
    public  Object              assetActive;
    private List<string>        _folioList = new List<string>();
    private List<GameObject>    _optBtnList = new List<GameObject>();
    private Text                _debugText;
    
    [SerializeField]
    private GameObject          _assetObject;

    void Awake() {
        // What platform are we on
        string _path = "Assets/Folios/";
        switch(Application.platform) {
            case RuntimePlatform.WindowsEditor: _path = "Assets/Folios/";                               break;
            case RuntimePlatform.Android:       _path = Application.persistentDataPath + "/Folios/";    break;
            case RuntimePlatform.IPhonePlayer:  _path = "dunno, work this out";                         break;
            default:                            _path = Application.persistentDataPath + "/Folios/";    break;
        }

        // Create directory if needed
        if(!Directory.Exists(_path)) {
            Directory.CreateDirectory(_path);       
            return;                                 // dont need to continue
        }
        
        _debugText = transform.parent.GetChild(0).gameObject.GetComponent<Text>();

        checkFolios(_path);
        drawButtons();
    }

    // Scan for available folios and add them to list
    void checkFolios(string path) {
        string[] _files = Directory.GetFiles(path);
        _folioList.Clear();                         // if need to redraw

        for(int i = 0; i < _files.Length; i++) {
            if(_files[i].EndsWith(".fd")) 
                _folioList.Add(_files[i]);
        }
    }

    // Draw buttons for each found folio
    void drawButtons() {
        GameObject _btnPrefab = (GameObject)Resources.Load("Prefabs/BtnFolioOption");
        Transform _parent = gameObject.transform.GetChild(0).GetChild(0);
        _optBtnList.Clear();                                                                        // if need to redraw

        for(int i = 0; i < _folioList.Count; i++) {
            _optBtnList.Add(Instantiate(_btnPrefab, Vector3.zero, Quaternion.identity, _parent));   // Vector3.zero because content component will adjust positions
            _optBtnList[i].GetComponentInChildren<Text>().text = _folioList[i];                     // change text to name
        }
    }

    private void openFolio() {
        if(_folioList.Count == 0) return;                                       // there is nothing to load

        folioActive = AssetBundle.LoadFromFile(_folioList[0]);                  // make this open last known folio
        folioAssets = folioActive.GetAllAssetNames();                           // store contents in array
        assetActive = folioActive.LoadAsset(folioAssets[0]);        // load first asset
        
        GameObject _testFrame = (GameObject)Resources.Load("Prefabs/testFrame");

        _debugText.GetComponent<Text>().text = $"Opened {_folioList[0]}";

        if(assetActive.GetType() == typeof(Texture2D)) {                        // png or jpg
			Mesh _newMesh = _testFrame.GetComponent<MeshFilter>().sharedMesh;		// assign mesh as the frame's mesh
			Texture2D _newTex  = (Texture2D)assetActive;						// convert to Texture2D and assign it
			
			_assetObject.GetComponent<MeshFilter>().mesh = _newMesh;	// pass it over to the controller
			_assetObject.GetComponent<MeshRenderer>().material.SetTexture("_myTexture", _newTex);		// pass it 
		}

    }
}
