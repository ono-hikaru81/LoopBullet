using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameSetting : MonoBehaviour {
	public enum Stars {
		Jimejime,
		Moon,
		Magmag,
	};
	static Stars star;
	static public Stars Star {
		get { return star; }
		set { star = value; }
	}
	static TitleManager.Screens sceneToLoad = TitleManager.Screens.Title;
	public static TitleManager.Screens SceneToLoad { get => sceneToLoad; set => sceneToLoad = value; }

	[HideInInspector] public const int MAX_PLAYER = 4;

	static List<GameObject> players = new List<GameObject> ();
	public static List<GameObject> Players { get => players; set => players = value; }
	static List<GameObject> cameras = new List<GameObject>();
	public static List<GameObject> Cameras { get => cameras; set => cameras = value; }

	static Material[] playerMaterials = new Material[MAX_PLAYER];
	public static Material[] PlayerMaterials { get => playerMaterials; set => playerMaterials = value; }
	static Texture[] playerIcons = new Texture[MAX_PLAYER];
	public static Texture[] PlayerIcons { get => playerIcons; set => playerIcons = value; }
	static Texture[] playerNumbers = new Texture[MAX_PLAYER];
	public static Texture[] PlayerNumbers { get => playerNumbers; set => playerNumbers = value; }

	// Start is called before the first frame update
	void Start () {
		for (int i = 0; i < MAX_PLAYER; i++) {
			playerMaterials[i] = (Material)Resources.Load ( "Prefabs/Actor/Materials/" + (i + 1).ToString () + "P" );
			playerIcons[i] = (Texture)Resources.Load ( "Prefabs/Actor/Icons/" + (i + 1).ToString () + "P" );
			playerNumbers[i] = (Texture)Resources.Load ( "Prefabs/Actor/" + (i + 1).ToString () + "P" );
		}
	}

	// Update is called once per frame
	void Update () {

	}

	static public void SetPlayerMaterial () {
		for (int i = 0; i < players.Count; i++) {
			var p = players.ToList ();
			p.Reverse ();
			p[i].GetComponent<MeshRenderer> ().material = playerMaterials[i];
		}
	}
}
