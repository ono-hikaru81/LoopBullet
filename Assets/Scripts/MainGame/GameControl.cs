using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {
    public Player[] players;

    public GameObject start;
    public GameObject countThree;
    public GameObject countTwo;
    public GameObject countOne;
    public GameObject finish;
    public GameObject result;

    public GameObject pause;

    Player winner;

    static bool isPause;
    static bool isEnd;

    public RawImage[] frames;

    public RawImage winnerBanner;
    public Texture[] winnerTex;

    public GameObject menu;

    GameSetting gs;

    // Start is called before the first frame update
    void Start () {
        start.SetActive( false );
        finish.SetActive( false );
        countThree.SetActive( false );
        countTwo.SetActive( false );
        countOne.SetActive( false );
        pause.SetActive( false );
        result.SetActive( false );
        menu.SetActive( false );
        StartCoroutine( StartLogo() );
        isPause = false;
        isEnd = false;
        gs = GameObject.Find( "GameSetting" ).GetComponent<GameSetting>();
        // 星の生成
        GameObject star;
        switch ( gs.Star ) {
            case GameSetting.Stars.Jimejime:
                star = ( GameObject ) Resources.Load( "Prefabs/St1_Jimejime" );
                break;
            case GameSetting.Stars.Moon:
                star = ( GameObject ) Resources.Load( "Prefabs/St2_Moon" );
                break;
            case GameSetting.Stars.Magmag:
                // star = ( GameObject ) Resources.Load( "Prefabs/St3_Magmag" );
                star = ( GameObject ) Resources.Load( "Prefabs/St2_Moon" ); // モデルが出来れば上を使用
                break;
            default:
                star = ( GameObject ) Resources.Load( "Prefabs/St1_Jimejime" );
                break;
        }
        star = Instantiate( star, new Vector3( 0, 0, 0 ), Quaternion.identity );
        foreach ( Player temp in players ) {
            temp.Planet = star;
        }
    }

    // Update is called once per frame
    void Update () {
        if ( menu.activeSelf == true ) return;

        if ( isEnd == false ) {
            // 生存者チェック
            int d = 0;
            for ( int i = 0; i < players.Length; i++ ) {
                if ( players[i].Hp <= 0 ) {
                    d++;
                }
                else {
                    winner = players[i];
                    winnerBanner.texture = winnerTex[i];
                }
            }

            if ( d >= players.Length - 1 ) {
                finish.SetActive( true );
                winner.TakeDamage = false;
                winner.DisableInput = true;
                isEnd = true;
                Invoke( "EndProc", 1.0f );
            }
        }
        // 決着がついている
        else {
            if ( Input.GetButtonDown( "Enter" ) ) {
                menu.SetActive( true );
            }
        }
    }

    void EndProc () {
        result.SetActive( true );
        menu.SetActive( false );
        for(int i = 0; i < frames.Length; i++ ) {
            if ( winner.killedPlayers.Peek() == null ) break;

            frames[i].texture = winner.killedPlayers.Dequeue();
        }
    }

    public void Pause ( bool value ) {
        isPause = value;
        if ( isPause == true ) {
            Time.timeScale = 0.0001f;
            pause.SetActive( true );
        }
        else {
            Time.timeScale = 1;
            pause.SetActive( false );
        }
    }

    IEnumerator StartLogo () {
        yield return new WaitForSeconds( 0.01f );

        foreach ( Player temp in players ) {
            temp.DisableInput = true;
        }

        countThree.SetActive( true );
        yield return new WaitForSeconds( 1.0f );
        countThree.SetActive( false );

        countTwo.SetActive( true );
        yield return new WaitForSeconds( 1.0f );
        countTwo.SetActive( false );

        countOne.SetActive( true );
        yield return new WaitForSeconds( 1.0f );
        countOne.SetActive( false );

        start.SetActive( true );
        yield return new WaitForSeconds( 0.5f );
        start.SetActive( false );

        foreach ( Player temp in players ) {
            temp.DisableInput = false;
        }
    }
}
