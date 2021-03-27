using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFrashing : MonoBehaviour
{
    public float frashInterval;   // 点滅する間隔
    float counter;    // 点滅の進行度

    Text text;
    Color colorShelf;   // 変更する色情報

    bool pause;    // 停止

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        colorShelf = text.color;
        colorShelf.a = 0;
        counter = 0;
        pause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ( pause == true ) return;

        counter += Time.deltaTime;

        // counter が目標値以上になったら色情報を入れ替える
        if( counter >= frashInterval ) {
            counter = 0;
            SwapColor();
        }
    }

    public void Pause ( bool value ) {
        pause = value;
        // 透明状態であれば可視状態にする
        if( colorShelf.a != 0 ) {
            SwapColor();
        }
    }

    void SwapColor () {
        // 保存している色情報と入れ替え
        Color temp = text.color;
        text.color = colorShelf;
        colorShelf = temp;
    }
}
