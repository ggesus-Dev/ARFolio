using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

public class PrepareBatch : AssetPostprocessor {
    // Before any asset is imported
    static void OnPreprocessAsset() {
        if(!Application.isBatchMode) return;

        bool _debug = false;
        string _folioPath = "Assets/Test";
        string _outputPath = "Assets/LocalDB";
        string _filePath = "";
        string[] _files = Directory.GetFiles(_folioPath);
        // user information

        if(_files.Length != 0) {
            for(int i = _files.Length - 1; i > 0; i--) {
                if(_debug) debugReport(0, _files[i]);

                if(_files[i]).EndsWith(".png") || _files[i].EndsWith(".jpg") || _files[i].EndsWith(".fbx")) {
                    _file = _files[i].Replace(@"/", @"\");
                    AssetImporter.GetAtPath(_file).SetAssetBundleNameAndVariant("ggesus-folio", "fd");
                    
                    if(_debug) debugReport(1, _file);
                }
            }
        }

        if(!System.IO.Directory.Exists(_outputPath)) 
            System.IO.Directory.CreateDirectory(_outputPath);
        
        BuildPipeline.BuildAssetBundles(_outputPath, BuildAssetBundlesOptions.None, BuildTarget.Android);

        if(_debug) 
            string _logPath = "C:/Users/hayde/AppData/Local/Unity/Editor/Editor.log";
			Application.OpenURL(_logPath);
    }

    static void debugReport(int type, var args[]) {
        string _message = "";
        
        switch(type) {
            case 0:     _message = $"LILIA: File being looked at is: {args[0]}";                                break;
            case 1:     _message = $"LILIA: File name {args[0]} was updated and added to ggesus-folio.fd";      break;
            default:    _message = "LILIA: Unknown report.";                                                    break; 
        }

        Debug.Log(_message);
    }
}