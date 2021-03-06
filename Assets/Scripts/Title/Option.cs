using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Option : MonoBehaviour {
	[SerializeField] AudioMixer am;

	// 設定項目
	[SerializeField] Slider masterVol;
	[SerializeField] Slider bgmVol;
	[SerializeField] Slider seVol;
	[SerializeField] Slider lighting;
	[SerializeField] Slider color;
	Slider[] sliders;
	int currentSelect;  // 設定中の項目

	[SerializeField] Text text;   // 説明文

	// ルール
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
		UpdateRuleUI ();

		masterVol.CurrentValue = GameSetting.Instance.MasterVol;
		bgmVol.CurrentValue = GameSetting.Instance.BgmVol;
		seVol.CurrentValue = GameSetting.Instance.SeVol;
	}

	// Update is called once per frame
	void Update () {

	}

	void UpdateSetting () {
		GameSetting.Instance.MasterVol = masterVol.CurrentValue;
		GameSetting.Instance.BgmVol = bgmVol.CurrentValue;
		GameSetting.Instance.SeVol = seVol.CurrentValue;
		GameSetting.Instance.SetVolume ();
		GameSetting.Instance.ColorVision = color.CurrentValue;
		GameSetting.Instance.SetColorVision ();
	}

	// 操作する項目の変更
	void ChangeItem () {
		for (int i = 0; i < sliders.Length; i++) {
			sliders[i].Control = false;
		}

		sliders[currentSelect].Control = true;
		text.text = sliders[currentSelect].Description;
	}

	void UpdateRuleUI () {
		if (page >= rules.Length || page < 0) {
			page = 0;
			rule.SetActive ( false );
		}

		ruleRI.texture = rules[page];
	}

	// -------------入力----------------------
	public void OnSelect ( InputValue value ) {
		var a = value.Get<Vector2> ();
		if (a.y != 0) {
			currentSelect += (a.y == -1) ? 1 : -1;

			currentSelect = UIFunctions.RevisionValue ( currentSelect, sliders.Length - 1 );
			SoundManager.Instance.PlaySE ( SoundManager.SE.Cursor );
			ChangeItem ();
		}
		else if (a.x != 0) {
			sliders[currentSelect].Exec ( a.x );
			SoundManager.Instance.PlaySE ( SoundManager.SE.Gauge );
			UpdateSetting ();
		}

	}

	public void OnEnter () {
		// ルール非表示なら
		if (rule.activeSelf == false) {
			rule.SetActive ( true );
		}
		else {
			page++;
			UpdateRuleUI ();
		}

		SoundManager.Instance.PlaySE ( SoundManager.SE.Next );
	}

	public void OnBack () {
		// ルール非表示なら
		if (rule.activeSelf == false) {
			TitleManager.Instance.ChangeScreen ( TitleManager.Screens.Menu );
		}
		else {
			page--;
			UpdateRuleUI ();
		}

		SoundManager.Instance.PlaySE ( SoundManager.SE.Back );
	}
}
