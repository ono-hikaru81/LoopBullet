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

    int currentSelect;

    GameSetting gs;

    // Start is called before the first frame update
    void Start () {
        gs = GameObject.Find( "GameSetting" ).GetComponent<GameSetting>();
        menus = new RawImage[]{
            again,
            stage,
            title
        };

        currentSelect = 0;
    }

    // Update is called once per frame
    void Update () {
        // 項目移動
        float axis = ad.GetAxisDown( "DVertical" );
        // 上
        if(axis >= 1 ) {
            currentSelect--;
            if(currentSelect < 0 ) {
                currentSelect = 0;
            }
        }
        // 下
        else if(axis <= -1 ) {
            currentSelect++;
            if( currentSelect >= menus.Length ) {
                currentSelect = menus.Length - 1;
            }
        }

        // 選択中の項目の処理
        if ( Input.GetButtonDown( "Enter" ) ) {
            if ( menus[currentSelect] == again ) {
                SceneManager.LoadScene( "MainGame" );
            }
            else if ( menus[currentSelect] == stage ) {
                gs.Scene = GameSetting.Scenes.Stage;
                SceneManager.LoadScene( "Title" );
            }
            else if ( menus[currentSelect] == title ) {
                gs.Scene = GameSetting.Scenes.Title;
                SceneManager.LoadScene( "Title" );
            }
        }

        // 選択中の項目のみ強調
        foreach(RawImage temp in menus){
            temp.color = new Color( 0.5f, 0.5f, 0.5f );
        }

        menus[currentSelect].color = new Color( 1.0f, 1.0f, 1.0f );
    }
}
