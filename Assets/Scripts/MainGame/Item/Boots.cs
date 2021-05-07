using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boots : MonoBehaviour {
	[SerializeField] float acceleratedPlayerSpeed;
	float timer;
	[SerializeField] float timeLimit;

	void Start () {
		timer = 0.0f;
	}

	void Update () {
		for (int i = 0; i < GameSetting.Instance.Players.ToArray ().Length; i++) {
			if (GameSetting.Instance.Players[i] != null) {
				if (GameSetting.Instance.Players[i].GetComponent<Player> ().IsSpeedUp == true) {
					timer += Time.deltaTime;
					GameSetting.Instance.Players[i].GetComponent<Player> ().Speed = acceleratedPlayerSpeed;
				}

				if (timer >= timeLimit) {
					GameSetting.Instance.Players[i].GetComponent<Player> ().IsSpeedUp = false;
					GameSetting.Instance.Players[i].GetComponent<Player> ().Speed = GameSetting.Instance.Players[i].GetComponent<Player> ().BaseSpeed;
					timer = 0.0f;
				}
			}
		}
	}
}
