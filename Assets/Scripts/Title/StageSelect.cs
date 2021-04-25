using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour {
	// 選択
	int currentSelect;

	// UI
	[SerializeField] RawImage jimejime;
	[SerializeField] RawImage moon;
	[SerializeField] RawImage magmag;
	RawImage[] stars;

	// Start is called before the first frame update
	void Start () {
		stars = new RawImage[]{
			jimejime,
			moon,
			magmag,
		};
		UpdateStarUI ( currentSelect );
	}

	// Update is called once per frame
	void Update () {

	}

	void UpdateStarUI ( int number ) {
		for (int i = 0; i < stars.Length; i++) {
			var c = stars[i].color;
			if (i == number) {
				stars[i].color = new Color ( c.r, c.g, c.b, 1.0f );
			}
			else {
				stars[i].color = new Color ( c.r, c.g, c.b, 0.5f );
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
		}
	}

	public void OnEnter () {
		if (stars[currentSelect] == jimejime) {
			GameSetting.Star = GameSetting.Stars.Jimejime;
		}
		else if (stars[currentSelect] == moon) {
			GameSetting.Star = GameSetting.Stars.Moon;
		}
		else if (stars[currentSelect] == magmag) {
			GameSetting.Star = GameSetting.Stars.Magmag;
		}

		GameSetting.SetPlayerMaterial ();

		SceneManager.LoadScene ( "MainGame" );
	}

	public void OnBack () => TitleManager.ChangeScreen ( TitleManager.Screens.Connect );
}
