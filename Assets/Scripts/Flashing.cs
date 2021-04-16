using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flashing : MonoBehaviour {
    [SerializeField] float flashInterval;
    float timer;
    RawImage ri;

    // Start is called before the first frame update
    void Start () {
        timer = flashInterval;
        ri = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update () {
        timer -= Time.deltaTime;
        if ( timer < 0 ) {
            timer = flashInterval;
            Color c = ri.color;
            c.a = (c.a == 0) ? 1.0f : 0.0f;
            ri.color = c;
        }
    }
}
