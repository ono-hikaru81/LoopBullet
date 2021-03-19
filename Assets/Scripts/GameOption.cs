﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameOption : MonoBehaviour {
    public GameObject cursor;
    public Text description;

    public GameObject[] options;
    public OptionData[] optionDatas;
    public int currentSelect;

    public AudioMixer mixer;

    // Start is called before the first frame update
    void Start () {
        currentSelect = 0;
        options[0].GetComponent<OptionData>().IsControl( true );
        for(int i = 0; i < options.Length; i++ ) {
            optionDatas[i] = options[i].GetComponent<OptionData>();
        }
    }

    // Update is called once per frame
    void Update () {
        if ( Input.GetKeyDown( KeyCode.UpArrow ) ) {
            currentSelect--;
            if ( currentSelect < 0 ) {
                currentSelect = 0;
            }

            DescriptionUpdate();
        }
        else if ( Input.GetKeyDown( KeyCode.DownArrow ) ) {
            currentSelect++;
            if ( currentSelect >= options.Length ) {
                currentSelect = options.Length - 1;
            }

            DescriptionUpdate();
        }

        // カーソルの位置調整
        Vector3 vec = options[currentSelect].transform.localPosition;
        vec.x = 0;
        cursor.transform.localPosition = vec;

        AudioControl();
    }

    void DescriptionUpdate () {
        description.text = optionDatas[currentSelect].GetDescription();

        foreach(OptionData temp in optionDatas ) {
            temp.IsControl( false );
        }

        optionDatas[currentSelect].IsControl( true );
    }

    void AudioControl () {
        switch ( optionDatas[currentSelect].GetOptionTag() ) {
            // 音量設定
            case OptionData.OptionTag.Master:
            case OptionData.OptionTag.BGM:
            case OptionData.OptionTag.SE:
                string str = optionDatas[currentSelect].GetOptionTag().ToString();
                float val = -80 + optionDatas[currentSelect].GetValue() * 8;
                mixer.SetFloat( str, val );
                break;
            default:
                break;
        }
    }
}