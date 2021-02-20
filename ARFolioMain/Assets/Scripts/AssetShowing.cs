using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetShowing : MonoBehaviour {
    void Awake() {
        ControllerGeneral.activeFolio += setupAsset;
        Debug.Log("bro");
    }

    private void setupAsset(ControllerGeneral.activeFolioData data) {
        var _activeFolio = data.folioActive;
        var _assetNames = data.folioAssetNames;
        var _activeAsset = data.assetActive;

        Debug.Log("Here");

        if(_activeAsset.GetType() == typeof(GameObject)) {
            Debug.Log("Type of image");
            setupAsImage(_activeAsset);
        }
    }

    private void setupAsImage(GameObject activeAsset) {
        GameObject _testFrame = (GameObject)Resources.Load("Prefabs/testFrame");
        Mesh _newMesh = _testFrame.GetComponent<MeshFilter>().sharedMesh;		// assign mesh as the frame's mesh
        
        Debug.Log("assigning mesh");
        gameObject.GetComponent<MeshFilter>().mesh = _newMesh;	                // pass it over to the controller
    }
}
