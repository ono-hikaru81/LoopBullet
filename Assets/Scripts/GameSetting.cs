using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour {
	public enum Stars {
		Jimejime,
		Moon,
		Magmag,
	};
	static Stars star;
	public Stars Star {
		get { return star; }
		set { star = value; }
	}

	public enum Scenes {
		Title,
		Menu,
		Option,
		Connect,
		Stage,
	};
	static Scenes scene = Scenes.Title;
	public Scenes Scene {
		get { return scene; }
		set { scene = value; }
	}

	[HideInInspector] public const int MAX_PLAYER = 4;

	public struct PlayerData {
		bool join;
		public bool Join {
			get { return join; }
			set { join = value; }
		}
		string horizontal;
		public string Horizontal {
			get { return horizontal; }
			set { horizontal = value; }
		}
		string vertical;
		public string Vertical {
			get { return vertical; }
			set { vertical = value; }
		}
		string shot;
		public string Shot {
			get { return shot; }
			set { shot = value; }
		}
		string item;
		public string Item {
			get { return item; }
			set { item = value; }
		}
	}
	static PlayerData[] buttonBinds = new PlayerData[MAX_PLAYER];
	public PlayerData[] ButtonBinds {
		get { return buttonBinds; }
		set { buttonBinds = value; }
	}

	// Start is called before the first frame update
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public int GetJoinedPlayers () {
		int t = 0;
		foreach (PlayerData temp in buttonBinds) {
			if (temp.Join == true) {
				t++;
			}
		}

		return t;
	}
}
