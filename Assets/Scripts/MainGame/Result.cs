using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Result : MonoBehaviour {
	[SerializeField] AxisDown ad;
	[SerializeField] GameSetting gs;
	[SerializeField] RawImage winnerBanner;   // 勝利プレイヤーを表示
	[SerializeField] GameObject menu; // リザルトメニュー
	[SerializeField] RawImage again;    // もう一度プレイ
	[SerializeField] RawImage stage;    // ステージ選択
	[SerializeField] RawImage title;    // タイトルへ
	RawImage[] menus;
	int currentSelect;
	bool showMenu;

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
	}

	// Update is called once per frame
	void Update () {
		if (showMenu == false) {
			if (Input.GetButtonDown ( "Enter" )) {
				showMenu = true;
				menu.SetActive ( true );
			}
		}
		else {
			// 項目移動
			float axis = ad.GetAxisDown ( "DVertical" );
			// 上
			if (axis >= 1) {
				currentSelect--;
				if (currentSelect < 0) {
					currentSelect = 0;
				}
			}
			// 下
			else if (axis <= -1) {
				currentSelect++;
				if (currentSelect >= menus.Length) {
					currentSelect = menus.Length - 1;
				}
			}

			// 選択中の項目の処理
			if (Input.GetButtonDown ( "Enter" )) {
				if (menus[currentSelect] == again) {
					SceneManager.LoadScene ( "MainGame" );
				}
				else if (menus[currentSelect] == stage) {
					gs.Scene = GameSetting.Scenes.Stage;
					SceneManager.LoadScene ( "Title" );
				}
				else if (menus[currentSelect] == title) {
					gs.Scene = GameSetting.Scenes.Title;
					SceneManager.LoadScene ( "Title" );
				}
			}

			// 選択中の項目のみ強調
			foreach (RawImage temp in menus) {
				temp.color = new Color ( 0.5f, 0.5f, 0.5f );
			}

			menus[currentSelect].color = new Color ( 1.0f, 1.0f, 1.0f );
		}
	}

	public void SetWinnerTex ( Player winner ) {
		winnerBanner.texture = winner.WinBanner;
	}

	public void SetWinnerTex ( Texture banner ) {
		winnerBanner.texture = banner;
	}
}
