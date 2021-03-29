using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slider : MonoBehaviour {
    public AxisDown ad;
    RawImage ri;
    RectTransform rt;

    public Texture select;      // ピンの画像: 選択中
    public Texture noSelect;    // ピンの画像: 非選択中

    public string description;  // 説明文
    public string Description {
        get { return description; }
        set { description = value; }
    }

    public float y; // Y座標
    public float begin; // 始点X
    public float end;   // 終点X

    public int max; // 最大値

    public int currentValue;    // 現在値
    public int CurrentValue {
        get { return currentValue; }
        set { currentValue = value; }
    }

    float valuePer; // 一回の移動量: X座標

    public bool control;    // 操作可能フラグ
    public bool Control {
        get { return control; }
        set {
            control = value;
            ri.texture = ( value == true ) ? select : noSelect;
        }
    }

    // Start is called before the first frame update
    void Start () {
        valuePer = ( end - begin ) / max;
        if( valuePer <= 0 ) {
            valuePer *= -1;
        }

        ri = GetComponent<RawImage>();
        rt = GetComponent<RectTransform>();

        ri.texture = ( control == true ) ? select : noSelect;
    }

    // Update is called once per frame
    void Update () {
        Exec();

        float x = begin + ( valuePer * currentValue );
        rt.localPosition = new Vector3( x, y, 0 );
    }

    void Exec () {
        if ( control == false ) return;

        // 左
        if ( ad.GetAxisDown( "DHorizontal" ) == -1 ) {
            currentValue--;
            if( currentValue < 0 ) {
                currentValue = 0;
            }
        }
        // 右
        else if ( ad.GetAxisDown( "DHorizontal" ) == 1 ) {
            currentValue++;
            if( currentValue > max ) {
                currentValue = max;
            }
        }
    }
}
