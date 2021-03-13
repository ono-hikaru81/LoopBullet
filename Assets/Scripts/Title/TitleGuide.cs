using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleGuide : MonoBehaviour
{
    public GameObject title;
    ImageFade titleFade;
    public GameObject firstAction;
    bool appeared;  // "PUSH ANY BUTTON!"が表示済みかのフラグ
    public GameObject start;
    public GameObject option;

    // Start is called before the first frame update
    void Start()
    {
        titleFade = title.GetComponent<ImageFade>();
        firstAction.SetActive( false );
        start.SetActive( false );
        option.SetActive( false );
        appeared = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ( titleFade.IsComplete() && appeared == false ) {
            appeared = true;
            firstAction.SetActive( true );
        }
    }

    public void ShowTitleMenu () {
        firstAction.SetActive( false );
        start.SetActive( true );
        option.SetActive( true );
    }
}
