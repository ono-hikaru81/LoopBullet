using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Text score;
    [SerializeField] RawImage item;
    [SerializeField] RawImage[] magazine;
    [SerializeField] Texture bullet;
    [SerializeField] Texture bulletEmpty;
    [SerializeField] RawImage result;

    // Start is called before the first frame update
    void Start()
    {
        Color c = result.color;
        c.a = 0;
        result.color = c;
        score.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        // Score
        score.text = player.Score.ToString();

        // Item

        // Bullet
        foreach (RawImage temp in magazine)
        {
            temp.texture = bulletEmpty;
        }

        for (int i = 0; i < player.Magazine; i++)
        {
            magazine[i].texture = bullet;
        }
    }
}
