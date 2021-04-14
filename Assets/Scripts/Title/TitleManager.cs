using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour {
    public GameObject title;
    public GameObject menu;
    public GameObject connect;
    public GameObject option;
    public RawImage connectRI;
    public RawImage optionRI;
    public GameObject stage;
    public RawImage jimejimeRI;
    public RawImage moonRI;
    public RawImage magmagRI;
    RawImage[] menus;
    RawImage[] stars;
    int currentSelect;

    public GameSetting gs;

    public AxisDown ad;

    // Start is called before the first frame update
    void Start () {
        currentSelect = 0;
        menus = new RawImage[] {
            connectRI,
            optionRI,
        };
        stars = new RawImage[] {
            jimejimeRI,
            moonRI,
            magmagRI,
        };
        ChangeScreen( gs.Scene );
        connectRI.color = new Color( 1.0f, 1.0f, 1.0f );
        optionRI.color = new Color( 0.5f, 0.5f, 0.5f );
        jimejimeRI.color = new Color( 1.0f, 1.0f, 1.0f );
        moonRI.color = new Color( 0.5f, 0.5f, 0.5f );
        magmagRI.color = new Color( 0.5f, 0.5f, 0.5f );
    }

    // Update is called once per frame
    void Update () {
        switch ( gs.Scene ) {
            case GameSetting.Scenes.Title:
                Title();
                break;
            case GameSetting.Scenes.Menu:
                Menu();
                break;
            case GameSetting.Scenes.Option:
                Option();
                break;
            case GameSetting.Scenes.Connect:
                Connect();
                break;
            case GameSetting.Scenes.Stage:
                Stage();
                break;
            default:
                break;
        }
    }

    void Title () {
        // 次の画面へ
        if ( Input.GetButtonDown( "Start" ) ) {
            gs.Scene = GameSetting.Scenes.Menu;
            ChangeScreen( gs.Scene );
        }
    }

    void Menu () {
        // 項目移動
        float axis = ad.GetAxisDown( "DVertical" );
        // 上
        if ( axis >= 1 ) {
            currentSelect--;
            if ( currentSelect < 0 ) {
                currentSelect = 0;
            }
        }
        // 下
        else if ( axis <= -1 ) {
            currentSelect++;
            if ( currentSelect >= menus.Length ) {
                currentSelect = menus.Length - 1;
            }
        }

        // 選択中の項目の処理
        if ( Input.GetButtonDown( "Enter" ) ) {
            if ( menus[currentSelect] == connectRI ) {
                gs.Scene = GameSetting.Scenes.Connect;
            }
            else if ( menus[currentSelect] == optionRI ) {
                gs.Scene = GameSetting.Scenes.Option;
            }
            ChangeScreen( gs.Scene );
        }
        else if ( Input.GetButtonDown( "Cancel" ) ) {
            gs.Scene = GameSetting.Scenes.Title;
            ChangeScreen( gs.Scene );
        }

        // 選択中の項目のみ強調
        foreach ( RawImage temp in menus ) {
            temp.color = new Color( 0.5f, 0.5f, 0.5f );
        }

        menus[currentSelect].color = new Color( 1.0f, 1.0f, 1.0f );
    }

    void Option () {
        // 前の画面へ
        if ( option.activeSelf == false ) {
            gs.Scene = GameSetting.Scenes.Menu;
            ChangeScreen( gs.Scene );
        }
    }

    void Connect () {
        // 次の画面へ
        if ( Input.GetButtonDown( "Enter" ) ) {
            if ( gs.GetJoinedPlayers() > 0 ) {
                gs.Scene = GameSetting.Scenes.Stage;
                ChangeScreen( gs.Scene );
            }
        }
        // 前の画面へ
        else if ( Input.GetButtonDown( "Cancel" ) ) {
            gs.Scene = GameSetting.Scenes.Menu;
            ChangeScreen( gs.Scene );
        }
    }

    void Stage () {
        // 項目移動
        float axis = ad.GetAxisDown( "DHorizontal" );
        // 左
        if ( axis <= -1 ) {
            currentSelect--;
            if ( currentSelect < 0 ) {
                currentSelect = 0;
            }
        }
        // 右
        else if ( axis >= 1 ) {
            currentSelect++;
            if ( currentSelect >= stars.Length ) {
                currentSelect = stars.Length - 1;
            }
        }

        // 選択中の項目の処理
        if ( Input.GetButtonDown( "Enter" ) ) {
            if ( stars[currentSelect] == jimejimeRI ) {
                gs.Star = GameSetting.Stars.Jimejime;
            }
            else if ( stars[currentSelect] == moonRI ) {
                gs.Star = GameSetting.Stars.Moon;
            }
            else if ( stars[currentSelect] == magmagRI ) {
                gs.Star = GameSetting.Stars.Magmag;
            }

            SceneManager.LoadScene( "MainGame" );
        }
        else if ( Input.GetButtonDown( "Cancel" ) ) {
            gs.Scene = GameSetting.Scenes.Connect;
            ChangeScreen( gs.Scene );
        }

        // 選択中の項目
        foreach ( RawImage temp in stars ) {
            temp.color = new Color( 0.5f, 0.5f, 0.5f );
        }

        stars[currentSelect].color = new Color( 1.0f, 1.0f, 1.0f );
    }

    // 表示する画面を切り替える
    void ChangeScreen ( GameSetting.Scenes name ) {
        title.SetActive( false );
        menu.SetActive( false );
        option.SetActive( false );
        connect.SetActive( false );
        stage.SetActive( false );

        switch ( name ) {
            case GameSetting.Scenes.Title:
                title.SetActive( true );
                break;
            case GameSetting.Scenes.Menu:
                menu.SetActive( true );
                break;
            case GameSetting.Scenes.Option:
                option.SetActive( true );
                break;
            case GameSetting.Scenes.Connect:
                connect.SetActive( true );
                break;
            case GameSetting.Scenes.Stage:
                stage.SetActive( true );
                break;
            default:
                break;
        }

        currentSelect = 0;
    }
}
