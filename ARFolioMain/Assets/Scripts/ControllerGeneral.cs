using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class ControllerGeneral : MonoBehaviour {
    public  List<string>    folioList = new List<string>();                     // list of all avaiable bundles
    private string          _path = "Assets/Folios/";

    public struct activeFolioData {
        public AssetBundle  folioActive;
        public string[]     folioAssetNames;
        public GameObject   assetActive;
    }

    public static event Action<activeFolioData> activeFolio = delegate{};    

    private void Awake() {
        switch(Application.platform) {                                          // What platform are we on
            case RuntimePlatform.WindowsEditor: _path = "Assets/Folios/";                               break;
            case RuntimePlatform.Android:       _path = Application.persistentDataPath + "/Folios/";    break;
            case RuntimePlatform.IPhonePlayer:  _path = "dunno, work this out";                         break;
            default:                            _path = Application.persistentDataPath + "/Folios/";    break;
        }

        if(!Directory.Exists(_path)) {                                          // check for directory
            Directory.CreateDirectory(_path);                                   // create path if it doesn't exist
            return;                                                             // dont need to continue
        } 
        
        CheckFolios();    
        SendFolioData(); 
    }

    public void CheckFolios() {                                                 // Check for available bundles and add them to our list
        string[] _files = Directory.GetFiles(_path);                            // Temp array for getting file names
        folioList.Clear();                                                      // clear for if need to redraw

        for(int i = 0; i < _files.Length; i++) {
            if(HandleFileCheck(_files[i])) folioList.Add(_files[i]);            // check to see if file is one of ours
        }
    }

    private bool HandleFileCheck(string file) {
        return (file.EndsWith(".fd")) ? true : false;                           // may extend this in the future to validate if it's one of our bundles
    }

    private void SendFolioData() {
        if(folioList.Count == 0) return;                                        // there is nothing to load

        AssetBundle _folioToActivate = AssetBundle.LoadFromFile(folioList[0]);  // what bundle we want to show
        string[] _folioAssetNames = _folioToActivate.GetAllAssetNames();        // what asset from the bundle we want to show
        GameObject _assetActive = _folioToActivate.LoadAsset(_folioAssetNames[0]) as GameObject;  // array of asset names found in bundle

        activeFolioData _newData = new activeFolioData() {                      // create new struct
            folioActive = _folioToActivate,                                     // make this open last known folio
            folioAssetNames = _folioAssetNames,                                 // store contents in array
            assetActive = _assetActive                                          // load first asset
        };

        activeFolio(_newData);
    }
}
