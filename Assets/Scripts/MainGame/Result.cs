using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Result : MonoBehaviour {
	[SerializeField] GameObject menu; // リザルトメニュー
	[SerializeField] RawImage again;    // もう一度プレイ
	[SerializeField] RawImage stage;    // ステージ選択
	[SerializeField] RawImage title;    // タイトルへ
	RawImage[] menus;
	int currentSelect;
	bool showMenu;
	[SerializeField] RawImage[] iconFrames;
	[SerializeField] RawImage[] numbers;
	[SerializeField] Score[] scoreUIs;

	// Start is called before the first frame update
	void Start () {
		menus = new RawImage[]{
			again,
			stage,
			title
		};

		currentSelect = 0;

		showMenu = false;
		menu.SetActive ( false );

		foreach (RawImage temp in menus) {
			temp.color = new Color ( 0.5f, 0.5f, 0.5f );
		}

		menus[currentSelect].color = new Color ( 1.0f, 1.0f, 1.0f );

		// スコア順に並べる
		var p = new List<Player> ();
		foreach (GameObject o in GameSetting.Players) {
			p.Add ( o.GetComponent<Player> () );
		}

		p = p.OrderBy ( x => x.Score ).ToList ();
		p.Reverse ();

		for (int i = 0; i < p.Count; i++) {
			var n = p[i].GetComponent<PlayerInput> ().playerIndex;
			iconFrames[i].texture = GameSetting.PlayerIcons[n];
			numbers[i].texture = GameSetting.PlayerNumbers[n];
			scoreUIs[i].UpdateUI ( p[i] );
		}
	}

	// Update is called once per frame
	void Update () {

	}

	// ----------------入力--------------------
	public void OnSelect ( InputValue value ) {
		var a = value.Get<Vector2> ();
		if (a.y != 0) {
			if (showMenu == true) {
				currentSelect += (a.y == -1) ? 1 : -1;

				currentSelect = UIFunctions.RevisionValue ( currentSelect, menus.Length - 1 );

				// 選択中の項目のみ強調
				foreach (RawImage temp in menus) {
					temp.color = new Color ( 0.5f, 0.5f, 0.5f );
				}

				menus[currentSelect].color = new Color ( 1.0f, 1.0f, 1.0f );
			}
		}
	}

	public void OnEnter () {
		if (showMenu == false) {
			// メニュー表示
			showMenu = true;
			menu.SetActive ( true );
		}
		else {
			// そのシーンへ
			if (menus[currentSelect] == again) {
				SceneManager.LoadScene ( "MainGame" );
			}
			else if (menus[currentSelect] == stage) {
				SceneManager.LoadScene ( "Title" );
				GameSetting.SceneToLoad = TitleManager.Screens.SelectStage;
			}
			else if (menus[currentSelect] == title) {
				SceneManager.LoadScene ( "Title" );
				GameSetting.SceneToLoad = TitleManager.Screens.Title;
			}
		}
	}

	public void OnBack () {
		if (showMenu == true) {
			// メニュー表示
			showMenu = false;
			menu.SetActive ( false );
		}
	}
}
