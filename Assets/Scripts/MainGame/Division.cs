using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Division : MonoBehaviour {
	[SerializeField] Texture division2;
	[SerializeField] Texture division4;
	[SerializeField] RectTransform timer;

	// Start is called before the first frame update
	void Start () {
		if (GameSetting.Players.Count <= 2) {
			GetComponent<RawImage> ().texture = division2;
			timer.localPosition = new Vector3 ( 0, 430, 0 );
		}
		else {
			GetComponent<RawImage> ().texture = division4;
			timer.localPosition = Vector3.zero;
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
