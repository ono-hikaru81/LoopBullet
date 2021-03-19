using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioClip audioClip;
    OptionData gameOption;

    // Start is called before the first frame update
    void Start()
    {
        gameOption = GetComponent<OptionData>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( gameOption.GetSelected() == true ) {
            if ( Input.GetKeyDown( KeyCode.RightArrow ) ||
                 Input.GetKeyDown( KeyCode.LeftArrow ) ) {
                GetComponent<AudioSource>().PlayOneShot( audioClip );
            }
        }
    }
}
