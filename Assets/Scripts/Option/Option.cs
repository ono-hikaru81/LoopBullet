using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Option : MonoBehaviour {
    // 設定項目
    public Slider masterVol;
    public Slider bgmVol;
    public Slider seVol;
    public Slider lighting;
    public Slider color;
    Slider[] sliders;

    public AudioMixer am;

    public AxisDown ad;
    public Text text;   // 説明文

    int currentSelect;  // 設定中の項目

    // Start is called before the first frame update
    void Start () {
        currentSelect = 0;
        sliders = new Slider[] {
            masterVol,
            bgmVol,
            seVol,
            lighting,
            color
        };
    }

    // Update is called once per frame
    void Update () {
        float axis = ad.GetAxisDown( "DVertical" );
        // 上キー
        if ( axis == 1 ) {
            currentSelect--;
            if ( currentSelect < 0 ) {
                currentSelect = 0;
            }

            ChangeItem();
        }
        // 下キー
        else if ( axis == -1 ) {
            currentSelect++;
            if ( currentSelect >= sliders.Length ) {
                currentSelect = sliders.Length - 1;
            }

            ChangeItem();
        }

        // 音量変更
        float vol = -80 + masterVol.CurrentValue * 8;
        am.SetFloat( "Master", vol );
        vol = -80 + bgmVol.CurrentValue * 8;
        am.SetFloat( "BGM", vol );
        vol = -80 + seVol.CurrentValue * 8;
        am.SetFloat( "SE", vol );
    }

    // 操作する項目の変更
    void ChangeItem () {
        for ( int i = 0; i < sliders.Length; i++ ) {
            sliders[i].Control = false;
        }

        sliders[currentSelect].Control = true;
        text.text = sliders[currentSelect].Description;
    }
}
