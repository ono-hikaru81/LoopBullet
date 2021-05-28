using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameSetting : Singleton<GameSetting> {
	[SerializeField] AudioMixer audioMixer;
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

	// オプション
	private int masterVol = 7;
	public int MasterVol { get => masterVol; set => masterVol = value; }
	private int bgmVol = 7;
	public int BgmVol { get => bgmVol; set => bgmVol = value; }
	private int seVol = 7;
	public int SeVol { get => seVol; set => seVol = value; }
	private int colorVision = 0;
	public int ColorVision { get => colorVision; set => colorVision = value; }
	[SerializeField] private Color trailColor = Color.green;
	public Color TrailColor { get => trailColor; set => trailColor = value; }

	// Start is called before the first frame update
	void Start () {
		for (int i = 0; i < MAX_PLAYER; i++) {
			playerMaterials[i] = (Material)Resources.Load ( "Prefabs/Actor/Materials/" + (i + 1).ToString () + "P" );
			playerIcons[i] = (Texture)Resources.Load ( "Prefabs/Actor/Icons/" + (i + 1).ToString () + "P" );
			playerNumbers[i] = (Texture)Resources.Load ( "Prefabs/Actor/" + (i + 1).ToString () + "P" );
		}
		SetVolume ();
		SetColorVision ();
	}

	// Update is called once per frame
	void Update () {

	}

	public void SetPlayerMaterial () {
		var p = players.ToList ();
		p.Reverse ();
		for (int i = 0; i < players.Count; i++) {
			var s = p[i].GetComponentsInChildren<SkinnedMeshRenderer> ();
			Material[] ma = { playerMaterials[i], playerMaterials[i] };
			foreach (var m in s) {
				m.materials = ma;
			}
		}
	}

	public void SetVolume () {
		float vol = (masterVol == 0) ? -80 : -40 + masterVol * 4;
		audioMixer.SetFloat ( "Master", vol );
		vol = (bgmVol == 0) ? -80 : -40 + bgmVol * 4;
		audioMixer.SetFloat ( "BGM", vol );
		vol = (seVol == 0) ? -80 : -40 + seVol * 4;
		audioMixer.SetFloat ( "SE", vol );
	}

	public void SetColorVision () {
		trailColor = colorVision switch
		{
			0 => Color.green,
			1 => Color.green,
			2 => Color.blue,
			3 => Color.red,
			_ => Color.green,
		};
	}
}
