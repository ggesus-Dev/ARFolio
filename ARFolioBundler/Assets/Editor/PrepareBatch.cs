using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

public class PrepareBatch : MonoBehaviour {
    // Before any asset is imported
    static void OnPreprocessAsset() {
        if(!Application.isBatchMode) return;

        bool _debug = false;
        string _folioPath = "";
        string _filePath = "";
        string[] _files = Directory.GetFiles(_folioPath);
        // user information

        if(_files.Length != 0) {
            for(int i = _files.Length - 1; i > 0; i--) {
                debugReport(0, _files[i]);
            }
        }


    }

    static void debugReport(int type, var args[]) {
        string _message = "";
        
        switch(type) {
            case 0:     _message = $"LILIA: File being looked at is: {args[i]}";    break;
            case 1:     _message = "LILIA: Testing";                                break;
            default:    _message = "LILIA: Unknown report.";                        break; 
        }

        Debug.Log(_message);
    }
}