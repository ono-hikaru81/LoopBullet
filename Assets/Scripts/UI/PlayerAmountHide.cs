using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmountHide : MonoBehaviour {
	[SerializeField] GameObject[] objects;

	// Start is called before the first frame update
	void Start () {
		for (int i = 0; i < objects.Length; i++) {
			if (i < GameSetting.Instance.Players.Count) {
				objects[i].SetActive ( true );
			}
			else {
				objects[i].SetActive ( false );
			}
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
