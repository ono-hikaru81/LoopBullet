using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour {
    public GameObject title;
    public GameObject menu;
    public RectTransform optionRT;
    public RectTransform connectRT;
    Vector2 defaultSize;
    Vector2 enlargeSize;
    public GameObject option;
    public GameObject connect;

    public enum Scenes {
        Title,
        Menu,
        Option,
        Connect
    };
    public Scenes scene;

    Scenes currentScene;

    public AxisDown ad;

    // Start is called before the first frame update
    void Start () {
        scene = Scenes.Title;
        currentScene = Scenes.Connect;
        ChangeScreen( scene );
        defaultSize.Set( 512, 100 );
        enlargeSize.Set( 640, 125 );
        connectRT.sizeDelta = enlargeSize;
        optionRT.sizeDelta = defaultSize;
    }

    // Update is called once per frame
    void Update () {
        switch ( scene ) {
            case Scenes.Title:
                Title();
                break;
            case Scenes.Menu:
                Menu();
                break;
            case Scenes.Option:
                Option();
                break;
            case Scenes.Connect:
                Connect();
                break;
            default:
                break;
        }
    }

    void Title () {
        // 次の画面へ
        if ( Input.GetButtonDown( "Start" ) ) {
            scene = Scenes.Menu;
            ChangeScreen( scene );
        }
    }

    void Menu () {
        float axis = ad.GetAxisDown( "DVertical" );
        // 上キー
        if ( axis == 1 ) {
            currentScene = Scenes.Connect;
            connectRT.sizeDelta = enlargeSize;
            optionRT.sizeDelta = defaultSize;
        }
        // 下キー
        else if ( axis == -1 ) {
            currentScene = Scenes.Option;
            connectRT.sizeDelta = defaultSize;
            optionRT.sizeDelta = enlargeSize;
        }
        // 選択しているメニューへ進む
        else if ( Input.GetButtonDown( "Enter" ) ) {
            scene = currentScene;
            ChangeScreen( currentScene );
        }
        // 前の画面へ
        else if ( Input.GetButtonDown( "Cancel" ) ) {
            scene = Scenes.Title;
            ChangeScreen( scene );
        }
    }

    void Option () {
        // 前の画面へ
        if ( Input.GetButtonDown( "Cancel" ) ) {
            scene = Scenes.Menu;
            ChangeScreen( scene );
        }
    }

    void Connect () {
        // 次の画面へ
        if ( Input.GetButtonDown( "Enter" ) ) {

        }
        // 前の画面へ
        else if ( Input.GetButtonDown( "Cancel" ) ) {
            scene = Scenes.Menu;
            ChangeScreen( scene );
        }
    }

    // 表示する画面を切り替える
    void ChangeScreen ( Scenes name ) {
        title.SetActive( false );
        menu.SetActive( false );
        option.SetActive( false );
        connect.SetActive( false );

        switch ( name ) {
            case Scenes.Title:
                title.SetActive( true );
                break;
            case Scenes.Menu:
                menu.SetActive( true );
                break;
            case Scenes.Option:
                option.SetActive( true );
                break;
            case Scenes.Connect:
                connect.SetActive( true );
                break;
            default:
                break;
        }
    }
}
