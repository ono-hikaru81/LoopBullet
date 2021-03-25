using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisDown : MonoBehaviour {
    public string[] axis;
    public float[] axisValue;
    float[] beforeValue;

    // Start is called before the first frame update
    void Start () {
        DontDestroyOnLoad( gameObject );
        System.Array.Resize( ref axisValue, axis.Length );
        System.Array.Resize( ref beforeValue, axis.Length );
    }

    // Update is called once per frame
    void Update () {
        for ( int i = 0; i < axis.Length; i++ ) {
            axisValue[i] = 0;
            if( beforeValue[i] == 0 ) {
                axisValue[i] = Input.GetAxisRaw( axis[i] );
            }

            beforeValue[i] = Input.GetAxisRaw( axis[i] );
        }
    }

    public float GetAxisDown ( string name ) {
        for ( int i = 0; i < axis.Length; i++ ) {
            if ( name == axis[i] ) {
                return axisValue[i];
            }
        }

        return 0;
    }
}
