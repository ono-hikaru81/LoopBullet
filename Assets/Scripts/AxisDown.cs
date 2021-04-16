using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AxisDown : MonoBehaviour {
    [SerializeField] string[] axis;   // 判定したい軸
    float[] axisValue;  // 軸の値
    float[] beforeValue;    // 前回の軸の値

    // Start is called before the first frame update
    void Start () {
        Array.Resize( ref axisValue, axis.Length );
        Array.Resize( ref beforeValue, axis.Length );
    }

    // Update is called once per frame
    void Update () {
        for ( int i = 0; i < axis.Length; i++ ) {
            axisValue[i] = 0;
            if ( beforeValue[i] == 0 ) {
                axisValue[i] = Input.GetAxisRaw( axis[i] );
            }

            beforeValue[i] = Input.GetAxisRaw( axis[i] );
        }
    }

    // 軸が入力された瞬間を判定
    public float GetAxisDown ( string name ) {
        for ( int i = 0; i < axis.Length; i++ ) {
            if ( name == axis[i] ) {
                return axisValue[i];
            }
        }

        return 0;
    }
}
