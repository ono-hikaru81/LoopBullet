using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slider : MonoBehaviour {
	[SerializeField] AxisDown ad;
	RawImage ri;
	RectTransform rt;

	[SerializeField] Texture select;      // ピンの画像: 選択中
	[SerializeField] Texture noSelect;    // ピンの画像: 非選択中

	[SerializeField] string description;  // 説明文
	public string Description {
		get { return description; }
		set { description = value; }
	}

	[SerializeField] float y; // Y座標
	[SerializeField] float begin; // 始点X
	[SerializeField] float end;   // 終点X

	[SerializeField] int max; // 最大値

	int currentValue;    // 現在値
	public int CurrentValue {
		get { return currentValue; }
		set { currentValue = value; }
	}

	float valuePer; // 一回の移動量: X座標

	bool control;    // 操作可能フラグ
	public bool Control {
		get { return control; }
		set {
			control = value;
			ri.texture = (value == true) ? select : noSelect;
		}
	}

	// Start is called before the first frame update
	void Start () {
		ri = GetComponent<RawImage> ();
		rt = GetComponent<RectTransform> ();

		valuePer = (end - begin) / max;
		if (valuePer <= 0) {
			valuePer *= -1;
		}

		ri.texture = (control == true) ? select : noSelect;
	}

	// Update is called once per frame
	void Update () {
		Exec ();

		float x = begin + (valuePer * currentValue);
		rt.localPosition = new Vector3 ( x, y, 0 );
	}

	void Exec () {
		if (control == false) return;

		var axis = ad.GetAxisDown ( "DHorizontal" );
		// 左
		if (axis == -1) {
			currentValue--;
			if (currentValue < 0) {
				currentValue = 0;
			}
		}
		// 右
		else if (axis == 1) {
			currentValue++;
			if (currentValue > max) {
				currentValue = max;
			}
		}
	}
}
