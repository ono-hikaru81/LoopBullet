using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Option : MonoBehaviour {
	[SerializeField] AxisDown ad;
	[SerializeField] AudioMixer am;
	// 設定項目
	[SerializeField] Slider masterVol;
	[SerializeField] Slider bgmVol;
	[SerializeField] Slider seVol;
	[SerializeField] Slider lighting;
	[SerializeField] Slider color;
	Slider[] sliders;
	[SerializeField] Text text;   // 説明文
	int currentSelect;  // 設定中の項目
	[SerializeField] GameObject rule;
	[SerializeField] RawImage ruleRI;
	[SerializeField] Texture[] rules;
	int page;

	// Start is called before the first frame update
	void Start () {
		rule.SetActive ( false );
		page = 0;
		currentSelect = 0;
		sliders = new Slider[] {
			masterVol,
			bgmVol,
			seVol,
			lighting,
			color
		};
		ChangeItem ();
	}

	// Update is called once per frame
	void Update () {
		if (rule.activeSelf == false) {
			OptionScene ();
		}
		else {
			RuleScene ();
		}
	}

	void OptionScene () {
		float axis = ad.GetAxisDown ( "DVertical" );
		// 上キー
		if (axis == 1) {
			currentSelect--;
			if (currentSelect < 0) {
				currentSelect = 0;
			}

			ChangeItem ();
		}
		// 下キー
		else if (axis == -1) {
			currentSelect++;
			if (currentSelect >= sliders.Length) {
				currentSelect = sliders.Length - 1;
			}

			ChangeItem ();
		}
		// 決定ボタン
		else if (Input.GetButtonDown ( "Enter" )) {
			rule.SetActive ( true );
		}
		else if (Input.GetButtonDown ( "Cancel" )) {
			gameObject.SetActive ( false );
		}

		// 音量変更
		float vol = -80 + masterVol.CurrentValue * 8;
		am.SetFloat ( "Master", vol );
		vol = -80 + bgmVol.CurrentValue * 8;
		am.SetFloat ( "BGM", vol );
		vol = -80 + seVol.CurrentValue * 8;
		am.SetFloat ( "SE", vol );
	}

	void RuleScene () {
		if (Input.GetButtonDown ( "Enter" )) {
			page++;
		}
		else if (Input.GetButtonDown ( "Cancel" )) {
			page--;
		}

		if (page >= rules.Length || page < 0) {
			page = 0;
			rule.SetActive ( false );
			return;
		}

		ruleRI.texture = rules[page];
	}

	// 操作する項目の変更
	void ChangeItem () {
		for (int i = 0; i < sliders.Length; i++) {
			sliders[i].Control = false;
		}

		sliders[currentSelect].Control = true;
		text.text = sliders[currentSelect].Description;
	}
}
