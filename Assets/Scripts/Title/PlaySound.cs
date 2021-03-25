using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioClip audioClip;
    OptionData gameOption;

    public GameObject axisInputMng;
    AxisDown ad;

    // Start is called before the first frame update
    void Start()
    {
        gameOption = GetComponent<OptionData>();
        ad = axisInputMng.GetComponent<AxisDown>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( gameOption.GetSelected() == true ) {
            if ( ad.GetAxisDown( "DHorizontal" ) != 0 ) {
                GetComponent<AudioSource>().PlayOneShot( audioClip );
            }
        }
    }
}
