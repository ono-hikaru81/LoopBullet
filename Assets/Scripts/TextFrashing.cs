using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFrashing : MonoBehaviour
{
    public int frashInterval;   // 点滅する間隔
    int counter;    // 点滅の進行度

    Text text;
    Color colorShelf;   // 変更する色情報

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        colorShelf = text.color;
        colorShelf.a = 0;
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate () {
        counter++;

        // counter が目標値以上になったら色情報を入れ替える
        if( counter >= frashInterval ) {
            counter = 0;
            // 現在の色と保存している色を入れ替え
            Color temp = text.color;
            text.color = colorShelf;
            colorShelf = temp;
        }
    }
}
