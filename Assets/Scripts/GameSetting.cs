using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameSetting : Singleton<GameSetting> {
	public enum Stars {
		Jimejime,
		Moon,
		Magmag,
	};
	Stars star;
	public Stars Star {
		get { return star; }
		set { star = value; }
	}
	TitleManager.Screens sceneToLoad = TitleManager.Screens.Title;
	public TitleManager.Screens SceneToLoad { get => sceneToLoad; set => sceneToLoad = value; }

	[HideInInspector] public const int MAX_PLAYER = 4;

	List<GameObject> players = new List<GameObject> ();
	public List<GameObject> Players { get => players; set => players = value; }
	List<GameObject> cameras = new List<GameObject> ();
	public List<GameObject> Cameras { get => cameras; set => cameras = value; }

	Material[] playerMaterials = new Material[MAX_PLAYER];
	public Material[] PlayerMaterials { get => playerMaterials; set => playerMaterials = value; }
	Texture[] playerIcons = new Texture[MAX_PLAYER];
	public Texture[] PlayerIcons { get => playerIcons; set => playerIcons = value; }
	Texture[] playerNumbers = new Texture[MAX_PLAYER];
	public Texture[] PlayerNumbers { get => playerNumbers; set => playerNumbers = value; }

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

	public void SetPlayerMaterial () {
		var p = players.ToList ();
		p.Reverse ();
		for (int i = 0; i < players.Count; i++) {
			var s = p[i].GetComponentsInChildren<SkinnedMeshRenderer> ();
			foreach (var m in s) {
				m.material = playerMaterials[i];
			}
		}
	}
}
