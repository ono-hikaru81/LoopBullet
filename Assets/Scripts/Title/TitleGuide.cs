using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleGuide : MonoBehaviour
{
    public GameObject title;
    ImageFade titleFade;
    public GameObject firstAction;

    public GameObject start;
    Text startText;
    public GameObject option;
    Text optionText;

    public GameObject axisInputMng;
    AxisDown ad;

    public ChangeScreen cs;

    float shadowAlpha;

    enum Progress {
        Opening,
        Title,
        Start,
        Option
    };
    Progress progress;

    // Start is called before the first frame update
    void Start()
    {
        titleFade = title.GetComponent<ImageFade>();
        firstAction.SetActive( false );

        startText = start.GetComponent<Text>();
        optionText = option.GetComponent<Text>();

        start.SetActive( false );
        option.SetActive( false );
        shadowAlpha = 0.5f;
        Color c = startText.color;
        startText.color = new Color( c.r, c.g, c.b, 1.0f );
        c = optionText.color;
        optionText.color = new Color( c.r, c.g, c.b, shadowAlpha );

        ad = axisInputMng.GetComponent<AxisDown>();

        progress = Progress.Title;
    }

    // Update is called once per frame
    void Update () {
        switch ( progress ) {
            case Progress.Opening:
                // スタートボタン or 演出終了で次の画面へ
                if ( Input.GetButtonDown( "Start" ) || titleFade.IsComplete() == true ) {
                    firstAction.SetActive( true );
                    titleFade.SkipFade();
                    progress = Progress.Title;
                }
                break;
            case Progress.Title:
                // スタートボタンで次の画面へ
                if ( Input.GetButtonDown( "Start" ) ) {
                    ShowTitleMenu();
                }
                break;
            case Progress.Start:
                // 下キー
                if ( ad.GetAxisDown( "DVertical" ) == -1 ) {
                    progress = Progress.Option;
                    SwapAlpha();
                }
                else if ( Input.GetButtonDown( "Enter" ) ) {
                    // 次のシーンへ
                }
                break;
            case Progress.Option:
                // 上キー
                if ( ad.GetAxisDown( "DVertical" ) == 1 ) {
                    progress = Progress.Start;
                    SwapAlpha();
                }
                else if ( Input.GetButtonDown( "Enter" ) ) {
                    // オプション表示
                    cs.Exec( 1 );
                }
                break;
            default:
                break;
        }
    }

    public void ShowTitleMenu () {
        firstAction.SetActive( false );
        progress = Progress.Start;
        start.SetActive( true );
        option.SetActive( true );
    }

    void SwapAlpha () {
        Color c1 = startText.color;
        Color c2 = optionText.color;
        startText.color = new Color( c1.r, c1.g, c1.b, c2.a );
        optionText.color = new Color( c2.r, c2.g, c2.b, c1.a );
    }
}
