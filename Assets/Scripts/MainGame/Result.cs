using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Result : MonoBehaviour {
	[SerializeField] GameObject menu; // リザルトメニュー
	[SerializeField] UIShadow again;    // もう一度プレイ
	[SerializeField] UIShadow stage;    // ステージ選択
	[SerializeField] UIShadow title;    // タイトルへ
	UIShadow[] menus;
	int currentSelect;
	bool showMenu;
	[SerializeField] GameObject[] modelPosList;
	[SerializeField] GameObject model;
	[SerializeField] RawImage[] numbers;
	[SerializeField] Score[] scoreUIs;

	// Start is called before the first frame update
	void Start () {
		menus = new UIShadow[]{
			again,
			stage,
			title
		};

		currentSelect = 0;

		showMenu = false;
		menu.SetActive ( false );

		UpdateUI ();

		// スコア順に並べる
		var p = GameControl.Instance.Rank;

		for (int i = 0; i < p.Count; i++) {
			var n = p[i].GetComponent<PlayerInput> ().playerIndex;
			// エモート用モデルを生成
			var o = Instantiate ( model, modelPosList[i].transform.position, modelPosList[i].transform.rotation );
			// テクスチャを貼る
			var s = o.GetComponentsInChildren<SkinnedMeshRenderer> ();
			var pm = GameSetting.Instance.PlayerMaterials[n];
			Material[] ma = { pm, pm };
			foreach (var m in s) {
				m.materials = ma;
			}
			o.GetComponent<Animator> ().SetInteger ( "rank", i + 1 );

			numbers[i].texture = GameSetting.Instance.PlayerNumbers[n];
			scoreUIs[i].UpdateUI ( p[i] );
		}
	}

	// Update is called once per frame
	void Update () {

	}

	void UpdateUI () {
		// 選択中の項目のみ強調
		foreach (var m in menus) {
			m.SetBrightMode ( UIShadow.BrightMode.Shadow );
		}

		menus[currentSelect].SetBrightMode ( UIShadow.BrightMode.Bright );
	}

	// ----------------入力--------------------
	public void OnSelect ( InputValue value ) {
		var a = value.Get<Vector2> ();
		if (a.y != 0) {
			if (showMenu == true) {
				currentSelect += (a.y == -1) ? 1 : -1;

				currentSelect = UIFunctions.RevisionValue ( currentSelect, menus.Length - 1 );
				SoundManager.Instance.PlaySE ( SoundManager.SE.Cursor );
				UpdateUI ();
			}
		}
	}

	public void OnEnter () {
		SoundManager.Instance.PlaySE ( SoundManager.SE.Next );

		if (showMenu == false) {
			// メニュー表示
			showMenu = true;
			menu.SetActive ( true );
		}
		else {
			Destroy ( GameControl.Instance.gameObject );
			var o = FindObjectOfType<Crown> ();
			Destroy ( o.gameObject );
			// そのシーンへ
			if (menus[currentSelect] == again) {
				SceneManager.LoadScene ( "MainGame" );
			}
			else if (menus[currentSelect] == stage) {
				SceneManager.LoadScene ( "Title" );
				GameSetting.Instance.SceneToLoad = TitleManager.Screens.SelectStage;
			}
			else if (menus[currentSelect] == title) {
				SceneManager.LoadScene ( "Title" );
				GameSetting.Instance.Players = new List<GameObject> ();
				GameSetting.Instance.SceneToLoad = TitleManager.Screens.Title;
			}
		}
	}

	public void OnBack () {
		if (showMenu == true) {
			// メニュー表示
			showMenu = false;
			SoundManager.Instance.PlaySE ( SoundManager.SE.Back );
			menu.SetActive ( false );
		}
	}
}
