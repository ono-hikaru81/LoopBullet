using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public enum Item {
        radar,
        speedBullet,
        heavyBullet,
        boots,        
        slowTimer,

        Max,
    };

    int itemNum;
    int randomNum;

    void Start() {
        itemNum = (int)Item.Max;
        randomNum = -1;
    }

    void Update() {
        for (int i = 0; i < GameSetting.Players.ToArray().Length; i++) {
            if(GameSetting.Players[i] != null) {
                if(GameSetting.Players[i].GetComponent<Player>().IsHitItemBox == true) {
                    randomNum = Random.Range(0, itemNum);

                    switch ((Item)randomNum) {
                        case Item.radar:
                            GameSetting.Players[i].GetComponent<Player>().UsableRadar = true;
                            break;
                        case Item.speedBullet:
                            GameSetting.Players[i].GetComponent<Player>().UsableSpdBullet = true;
                            break;
                        case Item.heavyBullet:
                            GameSetting.Players[i].GetComponent<Player>().UsableHevBullet = true;
                            break;
                        case Item.boots:
                            GameSetting.Players[i].GetComponent<Player>().UsableBoots = true;
                            break;
                        case Item.slowTimer:
                            GameSetting.Players[i].GetComponent<Player>().UsableSlowTimer = true;
                            break;
                    }
                    GameSetting.Players[i].GetComponent<Player>().IsHitItemBox = false;
                }
            }
        }
        Debug.Log(randomNum);
    }
}