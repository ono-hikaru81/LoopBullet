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

	Texture itemValue;
	[SerializeField] Texture radar;
	[SerializeField] Texture speedBullet;
	[SerializeField] Texture heavyBullet;
	[SerializeField] Texture boots;
	[SerializeField] Texture slowTimer;
	// Start is called before the first frame update
	void Start () {
		gameControl = FindObjectOfType<GameControl> ();
		Color c = result.color;
		c.a = 0;
		result.color = c;
	}

	// Update is called once per frame
	void Update () {
		// Rank
		for (int i = 0; i < gameControl.Rank.Count; i++) {
			if (gameControl.Rank[i] == player) {
				rank.texture = rankTexs[i];
			}
		}

		// Item
		for (int i = 0; i < GameSetting.Players.ToArray ().Length; i++) {
			if (GameSetting.Players[i] != null) {
				if (GameSetting.Players[i].GetComponent<Player> ().UsableRadar == true) {
					itemValue = radar;
				}
				else {
					itemValue = null;
				}

				if (GameSetting.Players[i].GetComponent<Player> ().UsableSpdBullet == true) {
					itemValue = speedBullet;
				}
				else {
					itemValue = null;
				}

				if (GameSetting.Players[i].GetComponent<Player> ().UsableHevBullet == true) {
					itemValue = heavyBullet;
				}
				else {
					itemValue = null;
				}

				if (GameSetting.Players[i].GetComponent<Player> ().UsableBoots == true) {
					itemValue = boots;
				}
				else {
					itemValue = null;
				}

				if (GameSetting.Players[i].GetComponent<Player> ().UsableSlowTimer == true) {
					itemValue = slowTimer;
				}
				else {
					itemValue = null;
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
}
