using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

public class CreateFolio : AssetPostprocessor {
    // Before any asset is imported
    static void OnPreprocessAsset() {
        // if(!Application.isBatchMode) return;
        if(!UnityEditorInternal.InternalEditorUtility.inBatchMode) return;

        bool _debug = false;
        string _folioPath = "Assets/Test";
        string _outputPath = "Assets/LocalDB";
        string _file = "";
        string _name = "ggesus-folio";
        string[] _files = Directory.GetFiles(_folioPath);
        // user information

        if(_files.Length != 0) {
            for(int i = _files.Length - 1; i > 0; i--) {
                if(_debug) debugReport(0, _files[i]);

                if((_files[i]).EndsWith(".png") || _files[i].EndsWith(".jpg") || _files[i].EndsWith(".fbx")) {
                    _file = _files[i].Replace(@"/", @"\");
                    AssetImporter.GetAtPath(_file).SetAssetBundleNameAndVariant(_name, "fd");
                    
                    if(_debug) debugReport(1, _file, _name);
                }
            }
        }

        if(!Directory.Exists(_outputPath)) 
            Directory.CreateDirectory(_outputPath);
        
        BuildPipeline.BuildAssetBundles(_outputPath, BuildAssetBundleOptions.None, BuildTarget.Android);

        if(_debug) {
            string _logPath = "C:/Users/hayde/AppData/Local/Unity/Editor/Editor.log";
            System.Diagnostics.Process.Start(_logPath);
        }
    }

    static void debugReport(int type, params string[] args) {
        string _message = "";
        
        switch(type) {
            case 0:     _message = $"LILIA: File being looked at is: {args[0]}";                                break;
            case 1:     _message = $"LILIA: File name {args[0]} was updated and added to {args[1]}.fd";         break;
            default:    _message = "LILIA: Unknown report.";                                                    break; 
        }

    System.Console.WriteLine(_message);
    }
}
