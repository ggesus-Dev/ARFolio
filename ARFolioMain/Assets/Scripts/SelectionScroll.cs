using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionScroll : MonoBehaviour {
    private GameObject _bar;            // reference to the scrollbar visual 
    private float[] _noOfChildren;      // number of children
    private float _distance;

    void Awake() {
        _bar = this.transform.parent.transform.parent.GetChild(1).gameObject;
        Debug.Log(_bar);
    }

    // saves us from constantly checking
    void OnEnable() {
        _noOfChildren = new float[transform.childCount];        // check how many children we have
        Debug.Log(_noOfChildren.Length);
    }

    // Update is called once per frame
    void Update() {
        _noOfChildren = new float[transform.childCount];        // check how many children we have
        Debug.Log(_noOfChildren.Length);

        // Space inbetween each option
        _distance = 1f / (_noOfChildren.Length - 1f);           // Max "Value" of bar (1f) divided by however many options there are

        // for however many children
        for(int i = 0; i < _noOfChildren.Length; i++) {
            _noOfChildren[i] = _distance * i;                   // What value each child is sitting at
        }

    }
}
