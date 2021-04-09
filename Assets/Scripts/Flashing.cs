using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flashing : MonoBehaviour {
    public float flashInterval;
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
            if ( c.a == 0 ) {
                c.a = 1.0f;
            }
            else {
                c.a = 0;
            }

            ri.color = c;
        }
    }
}
