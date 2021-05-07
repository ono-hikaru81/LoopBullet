using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {
	public enum Item {
		radar,
		speedBullet,
		heavyBullet,
		boots,
		slowTimer,

		Max,
	};

	int itemNum;
	int randomNum;

	void Start () {
		itemNum = (int)Item.Max;
		randomNum = -1;
	}

	void Update () {
		for (int i = 0; i < GameSetting.Instance.Players.ToArray ().Length; i++) {
			if (GameSetting.Instance.Players[i] != null) {
				if (GameSetting.Instance.Players[i].GetComponent<Player> ().IsHitItemBox == true) {
					randomNum = Random.Range ( 0, itemNum );

					switch ((Item)randomNum) {
						case Item.radar:
							GameSetting.Instance.Players[i].GetComponent<Player> ().UsableRadar = true;
							break;
						case Item.speedBullet:
							GameSetting.Instance.Players[i].GetComponent<Player> ().UsableSpdBullet = true;
							break;
						case Item.heavyBullet:
							GameSetting.Instance.Players[i].GetComponent<Player> ().UsableHevBullet = true;
							break;
						case Item.boots:
							GameSetting.Instance.Players[i].GetComponent<Player> ().UsableBoots = true;
							break;
						case Item.slowTimer:
							GameSetting.Instance.Players[i].GetComponent<Player> ().UsableSlowTimer = true;
							break;
					}
					GameSetting.Instance.Players[i].GetComponent<Player> ().IsHitItemBox = false;
				}
			}
		}
	}
}
