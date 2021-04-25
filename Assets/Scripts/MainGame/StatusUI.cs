using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour {
	Player player;
	public Player PlayerData { get => player; set => player = value; }
	[SerializeField] Score score;
	[SerializeField] RawImage item;
	[SerializeField] RawImage[] magazine;
	[SerializeField] Texture bullet;
	[SerializeField] Texture bulletEmpty;
	[SerializeField] RawImage result;

	// Start is called before the first frame update
	void Start () {
		Color c = result.color;
		c.a = 0;
		result.color = c;
	}

	// Update is called once per frame
	void Update () {
		// Score
		score.UpdateUI ( player );

		// Item

		// Bullet
		foreach (RawImage temp in magazine) {
			temp.texture = bulletEmpty;
		}

		for (int i = 0; i < player.Magazine; i++) {
			magazine[i].texture = bullet;
		}
	}
}
