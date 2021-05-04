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
	[SerializeField] UIShadow battle;
	[SerializeField] UIShadow option;
	UIShadow[] menus;

	// Start is called before the first frame update
	void Start () {
		menus = new UIShadow[]{
			battle,
			option,
		};
		screens = new Screen[]{
			Screen.Connect,
			Screen.Option,
		};

		StartCoroutine ( StartDeray () );
	}

	IEnumerator StartDeray () {
		yield return null;
		UpdateMenuUI ( currentSelect );
	}

	// Update is called once per frame
	void Update () {

	}

	void UpdateMenuUI ( int number ) {
		for (int i = 0; i < menus.Length; i++) {
			if (i == number) {
				menus[i].SetBrightMode ( UIShadow.BrightMode.Bright );
			}
			else {
				menus[i].SetBrightMode ( UIShadow.BrightMode.Shadow );
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
