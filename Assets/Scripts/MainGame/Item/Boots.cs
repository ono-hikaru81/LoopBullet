using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boots : MonoBehaviour
{
    [SerializeField] float acceleratedPlayerSpeed;
    float timer;
    [SerializeField] float timeLimit;

    void Start() {
        timer = 0.0f;
    }

    void Update() {
        for(int i = 0; i < GameSetting.Players.ToArray().Length; i++) {
            if (GameSetting.Players[i] != null) {
                if(GameSetting.Players[i].GetComponent<Player>().IsSpeedUp == true) {
                    timer += Time.deltaTime;
                    GameSetting.Players[i].GetComponent<Player>().Speed = acceleratedPlayerSpeed;
                }

                if(timer >= timeLimit) {
                    GameSetting.Players[i].GetComponent<Player>().IsSpeedUp = false;
                    GameSetting.Players[i].GetComponent<Player>().Speed = GameSetting.Players[i].GetComponent<Player>().BaseSpeed;
                    timer = 0.0f;
                }
            }
        }
    }
}
