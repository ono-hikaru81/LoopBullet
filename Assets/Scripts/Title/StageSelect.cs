using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour {
	// 選択
	int currentSelect;

	// UI
	[SerializeField] UIShadow jimejime;
	[SerializeField] UIShadow moon;
	[SerializeField] UIShadow magmag;
	UIShadow[] stars;

	// Start is called before the first frame update
	void Start () {
		stars = new UIShadow[]{
			jimejime,
			moon,
			magmag,
		};

		StartCoroutine ( StartDeray () );
	}

	IEnumerator StartDeray () {
		yield return null;
		UpdateStarUI ( currentSelect );
	}

	// Update is called once per frame
	void Update () {

	}

	void UpdateStarUI ( int number ) {
		for (int i = 0; i < stars.Length; i++) {
			if (i == number) {
				stars[i].SetBrightMode ( UIShadow.BrightMode.Bright );
			}
			else {
				stars[i].SetBrightMode ( UIShadow.BrightMode.Shadow );
			}
		}
	}

	// ------------入力--------------
	public void OnSelect ( InputValue value ) {
		var a = value.Get<Vector2> ();
		if (a.x != 0) {
			currentSelect += (a.x == -1) ? -1 : 1;

			currentSelect = UIFunctions.RevisionValue ( currentSelect, stars.Length - 1 );
			UpdateStarUI ( currentSelect );
			SoundManager.Instance.PlaySE ( SoundManager.SE.Cursor );
		}
	}

	public void OnEnter () {
		if (stars[currentSelect] == jimejime) {
			GameSetting.Instance.Star = GameSetting.Stars.Jimejime;
		}
		else if (stars[currentSelect] == moon) {
			GameSetting.Instance.Star = GameSetting.Stars.Moon;
		}
		else if (stars[currentSelect] == magmag) {
			GameSetting.Instance.Star = GameSetting.Stars.Magmag;
		}

		GameSetting.Instance.SetPlayerMaterial ();

		var o = FindObjectOfType<GameControl> ();
		Destroy ( o?.gameObject );
		SoundManager.Instance.PlaySE ( SoundManager.SE.Next );
		var p = FindObjectsOfType<Player> ();
		foreach (var c in p) {
			c.gameObject.GetComponentInChildren<Camera> ().enabled = true;
		}
		Destroy ( FindObjectOfType<TitleManager> ().gameObject );
		SceneManager.LoadScene ( "MainGame" );
	}

	public void OnBack () {
		SoundManager.Instance.PlaySE ( SoundManager.SE.Back );
		TitleManager.Instance.ChangeScreen ( TitleManager.Screens.Connect );
	}
}
