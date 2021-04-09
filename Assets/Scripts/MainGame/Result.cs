using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour {
    public AxisDown ad;
    public RawImage again;
    public RawImage stage;
    public RawImage title;

    RawImage[] menus;

    int currnetSelect;

    // Start is called before the first frame update
    void Start () {
        menus = new RawImage[]{
            again,
            stage,
            title
        };

        currnetSelect = 0;
    }

    // Update is called once per frame
    void Update () {
        // 項目移動
        float axis = ad.GetAxisDown( "DVertical" );
        // 上
        if(axis >= 1 ) {
            currnetSelect--;
            if(currnetSelect < 0 ) {
                currnetSelect = 0;
            }
        }
        // 下
        else if(axis <= -1 ) {
            currnetSelect++;
            if( currnetSelect >= menus.Length ) {
                currnetSelect = menus.Length - 1;
            }
        }

        // 選択中の項目の処理
        if ( Input.GetButtonDown( "Enter" ) ) {
            if ( menus[currnetSelect] == again ) {
                SceneManager.LoadScene( "MainGame" );
            }
            else if ( menus[currnetSelect] == stage ) {
                // ステージ選択へ
            }
            else if ( menus[currnetSelect] == title ) {
                SceneManager.LoadScene( "Title" );
            }
        }

        // 選択中の項目のみ強調
        foreach(RawImage temp in menus){
            temp.color = new Color( 0.5f, 0.5f, 0.5f );
        }

        menus[currnetSelect].color = new Color( 1.0f, 1.0f, 1.0f );
    }
}
