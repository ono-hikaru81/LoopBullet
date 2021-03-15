using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOption : MonoBehaviour {
    public GameObject cursor;

    public GameObject[] options;
    public int currentSelect;

    // Start is called before the first frame update
    void Start () {
        currentSelect = 0;
    }

    // Update is called once per frame
    void Update () {
        if ( Input.GetKeyDown( KeyCode.UpArrow ) ) {
        currentSelect--;
            if ( currentSelect < 0 ) {
                currentSelect = 0;
            }
        }
        else if ( Input.GetKeyDown( KeyCode.DownArrow ) ) {
            currentSelect++;
            if ( currentSelect >= options.Length ) {
                currentSelect = options.Length - 1;
            }
        }

        Vector3 temp = options[currentSelect].transform.localPosition;
        temp.x = 0;
        cursor.transform.localPosition = temp;
    }
}
