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
		Exit,
	};
	Screen[] screens;
	int currentSelect;

	// UI
	[SerializeField] UIShadow battle;
	[SerializeField] UIShadow option;
	[SerializeField] UIShadow exit;
	[SerializeField] YesorNo exitPopup;
	UIShadow[] menus;

	// Start is called before the first frame update
	void Start () {
		menus = new UIShadow[]{
			battle,
			option,
			exit,
		};
		screens = new Screen[]{
			Screen.Connect,
			Screen.Option,
			Screen.Exit,
		};

		exitPopup.gameObject.SetActive ( false );
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
		if (exitPopup.gameObject.activeSelf == true) {
			if (a.x != 0) {
				exitPopup.CurrentSelect += (a.x == -1) ? -1 : 1;
				exitPopup.CurrentSelect = UIFunctions.RevisionValue ( exitPopup.CurrentSelect, 2 );
				SoundManager.Instance.PlaySE ( SoundManager.SE.Cursor );
				exitPopup.UpdateUI ();
			}
		}
		else if (a.y != 0) {
			currentSelect += (a.y == -1) ? 1 : -1;

			currentSelect = UIFunctions.RevisionValue ( currentSelect, screens.Length - 1 );
			SoundManager.Instance.PlaySE ( SoundManager.SE.Cursor );
			UpdateMenuUI ( currentSelect );
		}
	}

	public void OnEnter () {
		if (exitPopup.gameObject.activeSelf == true) {
			// はい
			if (exitPopup.CurrentSelect == 0) {
				SoundManager.Instance.PlaySE ( SoundManager.SE.Next );
				Application.Quit ();
			}
			// いいえ
			else if (exitPopup.CurrentSelect == 1) {
				SoundManager.Instance.PlaySE ( SoundManager.SE.Back );
				exitPopup.gameObject.SetActive ( false );
			}
		}
		else {
			SoundManager.Instance.PlaySE ( SoundManager.SE.Next );

			if (screens[currentSelect] == Screen.Connect) {
				TitleManager.ChangeScreen ( TitleManager.Screens.Connect );
			}
			else if (screens[currentSelect] == Screen.Option) {
				TitleManager.ChangeScreen ( TitleManager.Screens.Option );
			}
			else if (screens[currentSelect] == Screen.Exit) {
				exitPopup.gameObject.SetActive ( true );
			}
		}

	}

	public void OnBack () {
		SoundManager.Instance.PlaySE ( SoundManager.SE.Back );

		if (exitPopup.gameObject.activeSelf == true) {
			exitPopup.gameObject.SetActive ( false );
		}
		else {
			TitleManager.ChangeScreen ( TitleManager.Screens.Title );
		}
	}
}
