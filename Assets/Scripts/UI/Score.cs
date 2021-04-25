using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
	[SerializeField] RawImage[] scoreRigid;
	[SerializeField] Texture[] numbers;

	// Start is called before the first frame update
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void UpdateUI ( Player player ) {
		var s = player.Score;
		if (s < 0) {
			foreach (var r in scoreRigid) {
				r.texture = numbers[0];
			}
			return;
		}

		for (int r = 0; r < scoreRigid.Length; r++) {
			var n = s % 10;
			scoreRigid[r].texture = numbers[n];
			s /= 10;
		}
	}
}
