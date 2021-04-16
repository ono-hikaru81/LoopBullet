using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
	Text t;
	[SerializeField] GameControl gc;

	// Start is called before the first frame update
	void Start () {
		t = GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {
		if (gc.Timer <= 0.01f) {
			t.text = "";
		}
		else {
			t.text = gc.Timer.ToString ();
		}
	}
}
