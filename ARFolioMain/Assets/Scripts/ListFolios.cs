using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ListFolios : MonoBehaviour {
    public  AssetBundle         folioActive;
    public  string[]            folioAssets;
    public  GameObject          assetActive;
    private List<string>        _folioList = new List<string>();
    private List<GameObject>    _optBtnList = new List<GameObject>();
    private Text                _debugText;
    
    public struct activeFolioData {
        public AssetBundle  folioActive;
        public string[]     folioAssetNames;
        public GameObject   assetActive;
    }

    public static event Action<activeFolioData> activeFolio = delegate{};

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

    private void unpackAssets() {
        if(_folioList.Count == 0) return;                                       // there is nothing to load

        activeFolioData _newData = new activeFolioData() {
            folioActive = AssetBundle.LoadFromFile(_folioList[0]),              // make this open last known folio
            folioAssetNames = folioActive.GetAllAssetNames(),                   // store contents in array
            assetActive = (GameObject)folioActive.LoadAsset(folioAssets[0])     // load first asset
        };

        activeFolio(_newData);
    }
}
