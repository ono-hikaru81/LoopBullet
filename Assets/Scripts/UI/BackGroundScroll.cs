using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundScroll : MonoBehaviour {
    RawImage ri;
    public float value;

    // Start is called before the first frame update
    void Start () {
        ri = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update () {
        Rect r = ri.uvRect;
        r.x += value * Time.deltaTime;
        ri.uvRect = r;
    }
}
