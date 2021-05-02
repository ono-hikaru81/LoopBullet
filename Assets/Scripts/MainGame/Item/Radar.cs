using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    float timer;
    [SerializeField] private float timeLimit;

    void Start() {
        timer = 0.0f;
    }

    void Update() {
        for (int i = 0; i < GameSetting.Players.ToArray().Length; i++) {
            if(GameSetting.Players[i] != null) {
                if(GameSetting.Players[i].GetComponent<Player>().IsStartedRadar == true) {
                    timer += Time.deltaTime;
                    Camera camera = GameSetting.Players[i].transform.FindChild("Camera").gameObject.GetComponent<Camera>();
                    camera.cullingMask |= (1 << LayerMask.NameToLayer("BulletTrail"));
                }
                else {
                    Camera camera = GameSetting.Players[i].transform.FindChild("Camera").gameObject.GetComponent<Camera>();
                    camera.cullingMask &= ~(1 << LayerMask.NameToLayer("BulletTrail"));
                }

                if(timer >= timeLimit) {
                    GameSetting.Players[i].GetComponent<Player>().IsStartedRadar = false;
                    timer = 0.0f;
                }
            }
        }
    }
}
