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

    public GameObject pause;

    Player winner;

    static bool isPause;

    // Start is called before the first frame update
    void Start () {
        start.SetActive( false );
        finish.SetActive( false );
        countThree.SetActive( false );
        countTwo.SetActive( false );
        countOne.SetActive( false );
        pause.SetActive( false );
        StartCoroutine( StartLogo() );
        isPause = false;
    }

    // Update is called once per frame
    void Update () {
        // 生存者チェック
        int d = 0;
        for ( int i = 0; i < players.Length; i++ ) {
            if ( players[i].Hp <= 0 ) {
                d++;
            }
            else {
                winner = players[i];
            }
        }

        if ( d >= players.Length - 1 ) {
            finish.SetActive( true );
            winner.TakeDamage = false;
            // シーン遷移
            // Invoke() で〇秒後に実行できる
        }
    }

    public void Pause ( bool value ) {
        isPause = value;
        if ( isPause == true ) {
            Time.timeScale = 0.01f;
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
