using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
	// 画面切り替え;
	enum Screen {
		Connect,
		Option,
	};
	Screen[] screens;
	int currentSelect;

	// UI
	[SerializeField] RawImage battle;
	[SerializeField] RawImage option;
	RawImage[] menus;

	// Start is called before the first frame update
	void Start () {
		menus = new RawImage[]{
			battle,
			option,
		};
		screens = new Screen[]{
			Screen.Connect,
			Screen.Option,
		};

		UpdateMenuUI ( currentSelect );
	}

	// Update is called once per frame
	void Update () {

	}

	void UpdateMenuUI ( int number ) {
		for (int i = 0; i < menus.Length; i++) {
			var c = menus[i].color;
			if (i == number) {
				menus[i].color = new Color ( c.r, c.g, c.b, 1.0f );
			}
			else {
				menus[i].color = new Color ( c.r, c.g, c.b, 0.5f );
			}
		}
	}

	// -------------------入力-------------------
	public void OnSelect ( InputValue value ) {
		var a = value.Get<Vector2> ();
		if (a.y != 0) {
			currentSelect += (a.y == -1) ? 1 : -1;

			currentSelect = UIFunctions.RevisionValue ( currentSelect, screens.Length - 1 );
			UpdateMenuUI ( currentSelect );
		}
	}

	public void OnEnter () {
		if (screens[currentSelect] == Screen.Connect) {
			TitleManager.ChangeScreen ( TitleManager.Screens.Connect );
		}
		else if (screens[currentSelect] == Screen.Option) {
			TitleManager.ChangeScreen ( TitleManager.Screens.Option );
		}
	}

	public void OnBack () => TitleManager.ChangeScreen ( TitleManager.Screens.Title );
}
