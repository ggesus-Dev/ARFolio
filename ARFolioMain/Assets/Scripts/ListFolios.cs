using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ListFolios : MonoBehaviour {
    public  GameObject          activeFolio;
    private List<GameObject>    _folioList = new List<GameObject>();

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
        
        GameObject  _btnPrefab = (GameObject)Resources.Load("Prefabs/BtnFolioOption");
        float       _xbuffer = 20f, 
                    _ybuffer = 20f,
                    _btnWidth = _btnPrefab.GetComponent<RectTransform>().rect.width, 
                    _btnHeight = _btnPrefab.GetComponent<RectTransform>().rect.height;
        string[]    _files = Directory.GetFiles(_path);

        for(int i = 0; i < _files.Length; i++) {
            if(_files[i].EndsWith(".fd")) {
                Debug.Log(_files[i]);
                Vector3 _pos = new Vector3(100f + _xbuffer * (i + _btnWidth), 100f + _ybuffer * (i + _btnHeight), 0);
                _folioList.Add(Instantiate(_btnPrefab, _pos, Quaternion.identity, this.transform));
            }
        }

    }

    void Update() {
        
    }
}
