using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOption : MonoBehaviour {
    public GameObject cursor;
    public Text description;

    public GameObject[] options;
    public int currentSelect;

    // Start is called before the first frame update
    void Start () {
        currentSelect = 0;
        options[0].GetComponent<OptionData>().IsControl( true );
    }

    // Update is called once per frame
    void Update () {
        if ( Input.GetKeyDown( KeyCode.UpArrow ) ) {
            currentSelect--;
            if ( currentSelect < 0 ) {
                currentSelect = 0;
            }

            DescriptionUpdate();
        }
        else if ( Input.GetKeyDown( KeyCode.DownArrow ) ) {
            currentSelect++;
            if ( currentSelect >= options.Length ) {
                currentSelect = options.Length - 1;
            }

            DescriptionUpdate();
        }

        // カーソルの位置調整
        Vector3 vec = options[currentSelect].transform.localPosition;
        vec.x = 0;
        cursor.transform.localPosition = vec;
    }

    void DescriptionUpdate () {
        description.text = options[currentSelect].GetComponent<OptionData>().GetDescription();
        foreach(GameObject temp in options ) {
            temp.GetComponent<OptionData>().IsControl( false );
        }

        options[currentSelect].GetComponent<OptionData>().IsControl( true );
    }
}
