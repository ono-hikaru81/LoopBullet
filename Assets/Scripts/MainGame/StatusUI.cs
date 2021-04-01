using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour {
    public Player player;
    public RawImage hp;
    public Texture[] hpSpr;
    public RawImage item;
    public RawImage[] magazine;
    public Texture bullet;
    public Texture bulletEmpty;
    public RawImage result;

    // Start is called before the first frame update
    void Start () {
        Color c = result.color;
        c.a = 0;
        result.color = c;
    }

    // Update is called once per frame
    void Update () {
        // HP
        int h = player.Hp - 1;
        if ( h < 0 ) {
            Color c = result.color;
            c.a = 1.0f;
            result.color = c;
        }
        else {
            if ( h >= hpSpr.Length ) {
                h = hpSpr.Length - 1;
            }

            hp.texture = hpSpr[h];
        }

        // Item

        // Bullet
        foreach(RawImage temp in magazine ) {
            temp.texture = bulletEmpty;
        }

        for( int i = 0; i < player.Magazine; i++ ) {
            magazine[i].texture = bullet;
        }
    }
}
