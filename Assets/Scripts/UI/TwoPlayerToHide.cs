using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPlayerToHide : MonoBehaviour {
	// Start is called before the first frame update
	void Start () {
		if (GameSetting.Instance.Players.Count <= 2) {
			gameObject.SetActive ( false );
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
