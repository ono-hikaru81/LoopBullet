using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour {
	float timer;
	[SerializeField] private float timeLimit;

	void Start () {
		timer = 0.0f;
	}

	void Update () {
		for (int i = 0; i < GameSetting.Instance.Players.ToArray ().Length; i++) {
			if (GameSetting.Instance.Players[i] != null) {
				if (GameSetting.Instance.Players[i].GetComponent<Player> ().IsStartedRadar == true) {
					timer += Time.deltaTime;
					Camera camera = GameSetting.Instance.Players[i].transform.Find ( "Camera" ).gameObject.GetComponent<Camera> ();
					camera.cullingMask |= (1 << LayerMask.NameToLayer ( "BulletTrail" ));
				}
				else {
					Camera camera = GameSetting.Instance.Players[i].transform.Find ( "Camera" ).gameObject.GetComponent<Camera> ();
					camera.cullingMask &= ~(1 << LayerMask.NameToLayer ( "BulletTrail" ));
				}

				if (timer >= timeLimit) {
					GameSetting.Instance.Players[i].GetComponent<Player> ().IsStartedRadar = false;
					timer = 0.0f;
				}
			}
		}
	}
}
