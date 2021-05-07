using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTimer : MonoBehaviour {
	[SerializeField] GameObject bullet;
	[SerializeField] float deceleratedPlayerSpeed;
	[SerializeField] float deceleratedBulletSpeed;
	float timer;
	[SerializeField] float timeLimit;

	void Start () {
		timer = 0.0f;
	}

	void Update () {
		for (int i = 0; i < GameSetting.Instance.Players.ToArray ().Length; i++) {
			if (GameSetting.Instance.Players != null) {
				if (GameSetting.Instance.Players[i].GetComponent<Player> ().IsStartedSlowTimer == true) {
					timer += Time.deltaTime;
					foreach (GameObject temp in GameSetting.Instance.Players) {
						temp.GetComponent<Player> ().Speed = deceleratedPlayerSpeed;
					}
					GameSetting.Instance.Players[i].GetComponent<Player> ().Speed = GameSetting.Instance.Players[i].GetComponent<Player> ().BaseSpeed;
					bullet.GetComponent<Bullet> ().Speed = deceleratedBulletSpeed;
					GameObject[] bullets = GameObject.FindGameObjectsWithTag ( "Bullet" );
					foreach (GameObject temp in bullets) {
						temp.GetComponent<Bullet> ().Speed = deceleratedBulletSpeed;
					}
				}

				if (timer >= timeLimit) {
					GameSetting.Instance.Players[i].GetComponent<Player> ().IsStartedSlowTimer = false;
					foreach (GameObject temp in GameSetting.Instance.Players) {
						temp.GetComponent<Player> ().Speed = deceleratedPlayerSpeed;
					}
					bullet.GetComponent<Bullet> ().Speed = bullet.GetComponent<Bullet> ().BaseSpeed;
					GameObject[] bullets = GameObject.FindGameObjectsWithTag ( "Bullet" );
					foreach (GameObject temp in bullets) {
						temp.GetComponent<Bullet> ().Speed = temp.GetComponent<Bullet> ().BaseSpeed;
					}
					timer = 0.0f;
				}
			}
		}
	}
}
