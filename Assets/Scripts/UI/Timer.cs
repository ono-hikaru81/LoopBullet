using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
	[SerializeField] Texture[] numbers;
	[SerializeField] Texture[] redNumbers;
	Texture[] texToUse;
	[SerializeField] RawImage[] digits;
	bool twoDigit = false;
	bool oneDigit = false;

	// Start is called before the first frame update
	void Start () {
		texToUse = numbers;
	}

	// Update is called once per frame
	void Update () {
		var t = (int)GameControl.Instance.Timer;

		if (t == GameControl.Instance.LastSpurtTime) {
			texToUse = redNumbers;
		}
		// ２桁用に位置調整
		else if (t <= 99 && twoDigit == false) {
			twoDigit = true;
			foreach (var r in digits) {
				var p = r.rectTransform.position;
				p.x -= r.rectTransform.sizeDelta.x / 2;
				r.rectTransform.position = p;
			}
			digits[2].color = new Color ( 0, 0, 0, 0 );
		}
		else if (t <= 9 && oneDigit == false) {
			oneDigit = true;
			foreach (var r in digits) {
				var p = r.rectTransform.position;
				p.x -= r.rectTransform.sizeDelta.x / 2;
				r.rectTransform.position = p;
			}
			digits[1].color = new Color ( 0, 0, 0, 0 );
		}

		for (int d = 0; d < digits.Length; d++) {
			var n = t % 10;
			digits[d].texture = texToUse[n];
			t /= 10;
		}
	}
}
