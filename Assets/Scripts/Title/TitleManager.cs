using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Subsystems;

public class TitleManager : Singleton<TitleManager> {
	public enum Screens {
		Title,
		Menu,
		Connect,
		Option,
		SelectStage,
	};
	string[] screenNameList;

	[SerializeField] GameObject generalCanvas;
	[SerializeField] GameObject title;
	[SerializeField] GameObject menu;
	[SerializeField] GameObject connect;
	[SerializeField] GameObject option;
	[SerializeField] GameObject selectStage;
	GameObject[] screens;

	// Start is called before the first frame update
	void Start () {
		screens = new GameObject[]{
			title,
			menu,
			connect,
			option,
			selectStage,
		};

		screenNameList = Enum.GetNames ( typeof ( Screens ) );

		ChangeScreen ( GameSetting.Instance.SceneToLoad );

		StartCoroutine ( StartDeray () );
	}

	IEnumerator StartDeray () {
		yield return null;
		SoundManager.Instance.PlayBGM ( SoundManager.BGM.Title );
	}

	// Update is called once per frame
	void Update () {

	}

	public void ChangeScreen ( Screens next ) {
		foreach (GameObject s in screens) {
			s.SetActive ( false );
		}

		var i = Array.FindIndex ( screenNameList, 0, x => x == next.ToString () );
		screens[i].SetActive ( true );
		generalCanvas.SetActive ( !connect.activeSelf );
	}
}
