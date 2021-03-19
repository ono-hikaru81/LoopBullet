using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionData : MonoBehaviour
{
    public string description;

    public GameObject slider;
    public GameObject ping;
    public int maxValue;
    public int value;
    int valuePer;
    RectTransform rect;
    bool selected;

    public enum OptionTag {
        Master,
        BGM,
        SE,
        Lighting,
        ColorVision
    };

    public OptionTag optionTag;

    // Start is called before the first frame update
    void Start () {
        rect = slider.GetComponent<RectTransform>();
        valuePer = ( int ) rect.sizeDelta.x / maxValue;
        selected = false;
    }

    // Update is called once per frame
    void Update()
    {
        Control();

        // ピンの位置調整
        Vector3 vec = slider.transform.localPosition;
        vec.x += valuePer * value;
        vec.x -= rect.sizeDelta.x / 2;
        ping.transform.localPosition = vec;
    }

    void Control () {
        if ( selected == false ) return;

        // ピンの操作
        if( Input.GetKeyDown( KeyCode.LeftArrow ) ) {
            value--;
            if( value < 0 ) {
                value = 0;
            }
        }
        else if ( Input.GetKeyDown( KeyCode.RightArrow ) ) {
            value++;
            if( value > maxValue ) {
                value = maxValue;
            }
        }
    }

    public string GetDescription () {
        return description;
    }

    public void IsControl ( bool value ) {
        selected = value;
    }

    public bool GetSelected () {
        return selected;
    }

    public OptionTag GetOptionTag () {
        return optionTag;
    }

    public int GetValue () {
        return value;
    }
}
