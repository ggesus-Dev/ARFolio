using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class ControllerGeneral : MonoBehaviour {
    public  AssetBundle     folioToActivate;                                    // what bundle do we want to show
    public  GameObject      assetActive;                                        // what asset from the bundle is active
    public  string[]        folioAssetNames;                                    // array of asset names from bundle
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
        
        checkFolios();    
        sendFolioData(); 
    }

    public void checkFolios() {                                                 // Check for available bundles and add them to our list
        string[] _files = Directory.GetFiles(_path);                            // Temp array for getting file names
        folioList.Clear();                                                      // clear for if need to redraw

        for(int i = 0; i < _files.Length; i++) {
            if(_files[i].EndsWith(".fd")) 
                folioList.Add(_files[i]);
        }
    }

    private void sendFolioData() {
        if(folioList.Count == 0) return;                                        // there is nothing to load

        activeFolioData _newData = new activeFolioData() {                      // create new struct
            folioActive = AssetBundle.LoadFromFile(folioList[0]),               // make this open last known folio
            folioAssetNames = folioToActivate.GetAllAssetNames(),               // store contents in array
            assetActive = (GameObject)folioToActivate.LoadAsset(folioAssetNames[0]) // load first asset
        };

        activeFolio(_newData);
    }
}
