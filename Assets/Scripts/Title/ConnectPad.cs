using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectPad : MonoBehaviour
{
    public ChangeScreen cs;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetButtonDown( "Enter" ) ) {
            // ステージ選択へ
        }
        else if ( Input.GetButtonDown( "Cancel" ) ) {
            // タイトルメニューへ戻る
            cs.Exec( 0 );
        }
    }
}
