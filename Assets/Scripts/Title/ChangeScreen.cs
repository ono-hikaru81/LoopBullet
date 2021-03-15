using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScreen : MonoBehaviour
{
    public GameObject[] screens;
    int currentScreen;

    private void Start () {
        foreach(GameObject temp in screens ) {
            temp.SetActive( false );
        }

        screens[0].SetActive( true );
        currentScreen = 0;
    }

    public void Exec ( int nextScreen ) {
        if ( nextScreen >= screens.Length || nextScreen < 0 ) nextScreen = screens.Length - 1;
        screens[nextScreen].SetActive( true );
        screens[currentScreen].SetActive( false );
    }
}
