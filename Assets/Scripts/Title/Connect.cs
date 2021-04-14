using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Connect : MonoBehaviour {
    public GameSetting gs;
    public GameObject[] icons;
    public GameObject[] unconnecteds;
    GameSetting.PlayerData[] binds;

    // Start is called before the first frame update
    void Start () {
        binds = new GameSetting.PlayerData[] {
            new GameSetting.PlayerData(false, "Horizontal1", "Vertical1", "Shot1", "Item1"),
            new GameSetting.PlayerData(false, "Horizontal2", "Vertical2", "Shot2", "Item2"),
            new GameSetting.PlayerData(false, "Horizontal3", "Vertical3", "Shot3", "Item3"),
            new GameSetting.PlayerData(false, "Horizontal4", "Vertical4", "Shot4", "Item4"),
        };

        for ( int i = 0; i < gs.ButtonBinds.Length; i++ ) {
            icons[i].SetActive( false );
            unconnecteds[i].SetActive( true );
            gs.ButtonBinds[i] = binds[i];
        }
    }

    // Update is called once per frame
    void Update () {
        if ( Input.GetButtonDown( "Connect1" ) ) {
            Entry( 0 );
        }

        if ( Input.GetButtonDown( "Connect2" ) ) {
            Entry( 1 );
        }

        if ( Input.GetButtonDown( "Connect3" ) ) {
            Entry( 2 );
        }

        if ( Input.GetButtonDown( "Connect4" ) ) {
            Entry( 3 );
        }
    }

    void Entry ( int num ) {
        if ( gs.ButtonBinds[num].Join == false ) {
            gs.ButtonBinds[num].Join = true;
            icons[num].SetActive( true );
            unconnecteds[num].SetActive( false );
        }
        else {
            gs.ButtonBinds[num].Join = false;
            icons[num].SetActive( false );
            unconnecteds[num].SetActive( true );
        }
    }
}
