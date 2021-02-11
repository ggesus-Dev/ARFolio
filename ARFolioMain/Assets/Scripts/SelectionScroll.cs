using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionScroll : MonoBehaviour {
    private GameObject _bar;            // reference to the scrollbar visual 
    private float _scrollPos;           // 
    private float[] _noOfChildren;      // number of children

    // saves us from constantly checking
    void OnEnable() {
        _noOfChildren = new float[transform.childCount];        // check how many children we have
    }

    // Update is called once per frame
    void Update() {
        
    }
}
