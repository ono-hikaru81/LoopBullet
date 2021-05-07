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
		for (int i = 0; i < GameSetting.Instance.Players.ToArray ().Length; i++) {
			if (GameSetting.Instance.Players[i] != null) {
				if (GameSetting.Instance.Players[i].GetComponent<Player> ().UsableRadar == false &&
					GameSetting.Instance.Players[i].GetComponent<Player> ().UsableSpdBullet == false &&
					GameSetting.Instance.Players[i].GetComponent<Player> ().UsableHevBullet == false &&
					GameSetting.Instance.Players[i].GetComponent<Player> ().UsableBoots == false &&
					GameSetting.Instance.Players[i].GetComponent<Player> ().UsableSlowTimer == false) {
					item.texture = noItem;
				}
				else if (GameSetting.Instance.Players[i].GetComponent<Player> ().UsableRadar == true) {
					item.texture = radar;
				}
				else if (GameSetting.Instance.Players[i].GetComponent<Player> ().UsableSpdBullet == true) {
					item.texture = speedBullet;
				}
				else if (GameSetting.Instance.Players[i].GetComponent<Player> ().UsableHevBullet == true) {
					item.texture = heavyBullet;
				}
				else if (GameSetting.Instance.Players[i].GetComponent<Player> ().UsableBoots == true) {
					item.texture = boots;
				}
				else if (GameSetting.Instance.Players[i].GetComponent<Player> ().UsableSlowTimer == true) {
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
}
