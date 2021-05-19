using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour {
	Player player;
	GameControl gameControl;
	public Player PlayerData { get => player; set => player = value; }
	[SerializeField] RawImage rank;
	[SerializeField] Texture[] rankTexs;
	[SerializeField] RawImage item;
	[SerializeField] RawImage[] magazine;
	[SerializeField] Texture bullet;
	[SerializeField] Texture bulletEmpty;
	[SerializeField] RawImage result;

	[SerializeField] Texture radar;
	[SerializeField] Texture speedBullet;
	[SerializeField] Texture heavyBullet;
	[SerializeField] Texture boots;
	[SerializeField] Texture slowTimer;
	[SerializeField] Texture noItem;
	[SerializeField] Score score;
	[SerializeField] RectTransform number;
	// Start is called before the first frame update
	void Start () {
		gameControl = FindObjectOfType<GameControl> ();
		Color c = result.color;
		c.a = 0;
		result.color = c;
	}

	// Update is called once per frame
	void Update () {
		// Score
		score.UpdateUI ( player );

		// Rank
		for (int i = 0; i < gameControl.Rank.Count; i++) {
			if (gameControl.Rank[i] == player) {
				rank.texture = rankTexs[i];
			}
		}

		// Item
		for (int i = 0; i < GameSetting.Instance.Players.ToArray ().Length; i++) {
			if (GameSetting.Instance.Players[i] != null) {
				if (player.UsableRadar == false &&
					player.UsableSpdBullet == false &&
					player.UsableHevBullet == false &&
					player.UsableBoots == false &&
					player.UsableSlowTimer == false) {
					item.texture = noItem;
				}
				else if (player.UsableRadar == true) {
					item.texture = radar;
				}
				else if (player.UsableSpdBullet == true) {
					item.texture = speedBullet;
				}
				else if (player.UsableHevBullet == true) {
					item.texture = heavyBullet;
				}
				else if (player.UsableBoots == true) {
					item.texture = boots;
				}
				else if (player.UsableSlowTimer == true) {
					item.texture = slowTimer;
				}
			}
		}

		// Bullet
		foreach (RawImage temp in magazine) {
			temp.texture = bulletEmpty;
		}

		for (int i = 0; i < player.Magazine; i++) {
			magazine[i].texture = bullet;
		}
	}

	public void HideScore () {
		score.HideScore ();
	}

	public void SetNumberPos ( int playerNum ) {
		// 2人以下の場合
		var p = number.localPosition;
		p.y = playerNum switch
		{
			2 => 270,
			_ => 470,
		};
		number.localPosition = p;
	}
}
