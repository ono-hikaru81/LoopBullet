using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameControl : Singleton<GameControl> {
	[SerializeField] GameObject[] playerUIList;
	StatusUI[] statusUIs;
	List<Player> rank = new List<Player> ();
	public List<Player> Rank { get => rank; set => rank = value; }
	[SerializeField] GameObject[] st1SpawnPoints;
	[SerializeField] GameObject[] st2SpawnPoints;
	[SerializeField] GameObject[] st3SpawnPoints;
	struct SpawnPoint {
		Vector3 pos;
		Quaternion rot;
		bool used;

		public Vector3 Pos { get => pos; set => pos = value; }
		public Quaternion Rot { get => rot; set => rot = value; }
		public bool Used { get => used; set => used = value; }
	}
	SpawnPoint[] spawnPoints;

	// イントロ
	[SerializeField] GameObject start;
	[SerializeField] GameObject[] countNum;
	[SerializeField] GameObject finish;

	[SerializeField] GameObject mainCanvas;
	[SerializeField] GameObject result;
	[SerializeField] GameObject pause;

	bool isPause;    // ポーズフラグ
	bool isStart;    // ゲーム開始フラグ
	bool isEnd;  // ゲーム終了フラグ

	// 時間
	[SerializeField] GameObject timerObj;
	[SerializeField] float playTime;    // プレイ時間
	float timer;
	public float Timer {
		get { return timer; }
		set { timer = value; }
	}

	[SerializeField] int lastSpurtTime;
	public int LastSpurtTime { get => lastSpurtTime; }

	[SerializeField] Crown crown;

	// Start is called before the first frame update
	void Start () {
		timer = playTime;
		timerObj.SetActive ( true );
		start.SetActive ( false );
		finish.SetActive ( false );
		foreach (var c in countNum) {
			c.SetActive ( false );
		}
		mainCanvas.SetActive ( true );
		pause.SetActive ( false );
		result.SetActive ( false );
		StartCoroutine ( StartLogo () );
		isPause = false;
		isStart = false;
		isEnd = false;

		foreach (var p in GameSetting.Instance.Players) {
			p.GetComponent<Player> ().ShowScore = GameObject.Find ( "ScoreMng" ).GetComponent<ShowScore> ();
		}

		statusUIs = new StatusUI[playerUIList.Length];
		for (int i = 0; i < playerUIList.Length; i++) {
			statusUIs[i] = playerUIList[i].GetComponent<StatusUI> ();
		}

		// 星の生成
		GameObject star;
		GameObject[] sp;
		switch (GameSetting.Instance.Star) {
			case GameSetting.Stars.Jimejime:
				star = (GameObject)Resources.Load ( "Prefabs/St1_Jimejime" );
				sp = st1SpawnPoints;
				break;
			case GameSetting.Stars.Moon:
				star = (GameObject)Resources.Load ( "Prefabs/St2_Moon" );
				sp = st2SpawnPoints;
				break;
			case GameSetting.Stars.Magmag:
				star = (GameObject)Resources.Load ( "Prefabs/St3_Magmag" );
				sp = st3SpawnPoints;
				break;
			default:
				star = (GameObject)Resources.Load ( "Prefabs/St1_Jimejime" );
				sp = st1SpawnPoints;
				break;
		}
		star = Instantiate ( star, new Vector3 ( 0, 0, 0 ), Quaternion.identity );

		spawnPoints = new SpawnPoint[sp.Length];
		for (int i = 0; i < sp.Length; i++) {
			var s = sp[i];
			spawnPoints[i].Pos = s.transform.position;
			spawnPoints[i].Rot = s.transform.rotation;
			spawnPoints[i].Used = false;
		}

		// プレイヤー設定
		for (int i = 0; i < GameSetting.Instance.Players.Count; i++) {
			var n = GameSetting.Instance.Players[i].GetComponent<PlayerInput> ().playerIndex;
			GameSetting.Instance.Players[i].GetComponent<Player> ().Planet = star;
			playerUIList[n].GetComponent<StatusUI> ().PlayerData = GameSetting.Instance.Players[i].GetComponent<Player> ();
			playerUIList[n].SetActive ( true );
			if (GameSetting.Instance.Players.Count <= 2) {
				playerUIList[n].GetComponent<StatusUI> ().SetNumberPos ( GameSetting.Instance.Players.Count );
			}

			// スポーン地点
			SpawnPoint s;
			while (true) {
				var r = Random.Range ( 0, spawnPoints.Length );
				if (spawnPoints[r].Used == false) {
					s = spawnPoints[r];
					spawnPoints[r].Used = true;
					break;
				}
			}
			GameSetting.Instance.Players[i].transform.position = s.Pos;
			GameSetting.Instance.Players[i].transform.rotation = s.Rot;
			GameSetting.Instance.Players[i].GetComponent<Player> ().Reset ();
		}

		foreach (GameObject o in GameSetting.Instance.Players) {
			Rank.Add ( o.GetComponent<Player> () );
		}

		UpdateRank ();
		crown.Reset ();
		crown.UnsetMaster ();

		SoundManager.Instance.PlayBGM ( SoundManager.BGM.MainGame );
	}

	// Update is called once per frame
	void Update () {
		// タイマー
		if (isStart == true && isEnd == false && isPause == false) {
			timer -= Time.deltaTime;
		}

		if (timer < 0) {
			isEnd = true;
		}
		else if ((int)timer == LastSpurtTime) {
			foreach (var u in statusUIs) {
				u.HideScore ();
			}
		}

		if (isEnd == true && finish.activeSelf == false && result.activeSelf == false) {    // ゲーム終了したら
			SoundManager.Instance.PlaySE ( SoundManager.SE.Finish );
			finish.SetActive ( true );
			// 1秒後にもろもろ表示する
			Invoke ( "EndProc", 1.0f );
		}
	}

	void EndProc () {
		foreach (var p in GameSetting.Instance.Players) {
			p.GetComponent<Player> ().DisableInput = true;
		}

		SoundManager.Instance.PlayBGM ( SoundManager.BGM.Result );
		timerObj.SetActive ( false );
		finish.SetActive ( false );
		mainCanvas.SetActive ( false );
		result.SetActive ( true );
	}

	public void Pause ( bool value ) {
		if (isStart == false) return;

		isPause = value;
		if (isPause == true) {
			Time.timeScale = 0.0001f;
			pause.SetActive ( true );
		}
		else {
			Time.timeScale = 1;
			pause.SetActive ( false );
		}
	}

	IEnumerator StartLogo () {
		yield return new WaitForSeconds ( 0.01f );
		foreach (var p in GameSetting.Instance.Players) {
			p.GetComponent<Player> ().DisableInput = true;
		}

		SoundManager.Instance.PlaySE ( SoundManager.SE.Count );
		countNum[2].SetActive ( true );
		yield return new WaitForSeconds ( 1.0f );
		countNum[2].SetActive ( false );

		countNum[1].SetActive ( true );
		yield return new WaitForSeconds ( 1.0f );
		countNum[1].SetActive ( false );

		countNum[0].SetActive ( true );
		yield return new WaitForSeconds ( 1.0f );
		countNum[0].SetActive ( false );

		SoundManager.Instance.PlaySE ( SoundManager.SE.Start );
		start.SetActive ( true );
		yield return new WaitForSeconds ( 0.5f );
		start.SetActive ( false );

		isStart = true;
		foreach (var p in GameSetting.Instance.Players) {
			p.GetComponent<Player> ().DisableInput = false;
		}
	}

	public void UpdateRank () {
		// 順位の更新
		rank = Rank.OrderBy ( x => x.Score ).ToList ();
		rank.Reverse ();
		crown.ChangeMaster ( rank.First ().transform );
	}
}
