using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Connect : MonoBehaviour {
	[SerializeField] GameObject[] icons;

	// 入力
	PlayerInputManager playerInputManager;
	bool enterRelease;
	bool backRelease;

	// Start is called before the first frame update
	void Start () {
		playerInputManager = GetComponent<PlayerInputManager> ();
	}

	// Update is called once per frame
	void Update () {
		// 長押し対策
		if (CanReleasedEnter ()) {
			enterRelease = true;
		}
		else if (CanReleasedBack ()) {
			backRelease = true;
		}

		playerInputManager.EnableJoining ();

		// UIの更新
		var players = FindObjectsOfType<Player> ();
		foreach (var i in icons) {
			i.SetActive ( false );
		}

		for (int n = 0; n < players.Length; n++) {
			var i = players[n].GetComponent<PlayerInput> ().playerIndex;
			icons[i].SetActive ( true );
		}

		// 接続するとPlayerInputが使えなくなるので手動
		if (CanPressedEnter () && players.Length > 0) {
			GameSetting.Players = new List<GameObject> ();
			foreach (var p in players) {
				GameSetting.Players.Add ( p.gameObject );
				DontDestroyOnLoad ( p.gameObject );
			}

			enterRelease = false;
			backRelease = false;

			TitleManager.ChangeScreen ( TitleManager.Screens.SelectStage );
		}
		else if (CanPressedBack ()) {
			foreach (var p in players) {
				Destroy ( p.gameObject );
			}
			GameSetting.Players = new List<GameObject> ();

			enterRelease = false;
			backRelease = false;

			TitleManager.ChangeScreen ( TitleManager.Screens.Menu );
		}
	}

	bool CanReleasedEnter () => Keyboard.current.cKey.wasReleasedThisFrame || Gamepad.current.buttonEast.wasReleasedThisFrame;
	bool CanReleasedBack () => Keyboard.current.xKey.wasReleasedThisFrame || Gamepad.current.buttonSouth.wasReleasedThisFrame;

	bool CanPressedEnter () => (Keyboard.current.cKey.isPressed || Gamepad.current.buttonEast.isPressed) && enterRelease;

	bool CanPressedBack () => (Keyboard.current.xKey.isPressed || Gamepad.current.buttonSouth.isPressed) && backRelease;
}
