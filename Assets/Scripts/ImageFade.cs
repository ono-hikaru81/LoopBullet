using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFade : MonoBehaviour {
    public enum FadeMode {
        In,
        Out
    };

    public FadeMode fadeMode;   // フェードの方向
    public float completeTime;  // フェードが完了するまでにかかる時間
    float fadeSpeed;    // 1フレームに増える値の量
    float alpha;    // 現在のアルファ値
    bool complete;  // 完了しているか

    Image image;

    // Start is called before the first frame update
    void Start () {
        image = GetComponent<Image>();
        fadeSpeed = 1.0f / completeTime / 60;
        fadeSpeed *= ( fadeMode == FadeMode.In ) ? 1 : -1;
        alpha = ( fadeMode == FadeMode.In ) ? 0.0f : 1.0f;
        complete = false;
    }

    void FixedUpdate () {
        if ( complete ) return;

        Color temp = image.color;
        alpha += fadeSpeed;
        if ( fadeMode == FadeMode.In ) {
            if ( alpha >= 1.0f ) {
                complete = true;
                alpha = 1.0f;
            }
        }
        else {
            if ( alpha <= 0.0f ) {
                complete = true;
                alpha = 0.0f;
            }
        }

        temp.a = alpha;
        image.color = temp;
    }

    public bool IsComplete () {
        return complete;
    }

    public void SkipFade () {
        alpha = ( fadeMode == FadeMode.In ) ? 1.0f : 0.0f;
        Color temp = image.color;
        temp.a = alpha;
        image.color = temp;
        complete = true;
    }
}
