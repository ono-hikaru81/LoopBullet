using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {
	[SerializeField] GameObject[] playerUIList;
	List<Player> rank = new List<Player> ();
	public List<Player> Rank { get => rank; set => rank = value; }

	// イントロ
	[SerializeField] GameObject start;
	[SerializeField] GameObject[] countNum;
	[SerializeField] GameObject finish;
	[SerializeField] GameObject result;
	[SerializeField] GameObject pause;

	static bool isPause;    // ポーズフラグ
	static bool isStart;    // ゲーム開始フラグ
	static bool isEnd;  // ゲーム終了フラグ

	// 時間
	[SerializeField] GameObject timerObj;
	[SerializeField] float playTime;    // プレイ時間
	float timer;
	public float Timer {
		get { return timer; }
		set { timer = value; }
	}

	[SerializeField] Texture[] numbers;
	[SerializeField] RawImage[] timers;

	// Start is called before the first frame update
	void Start () {
		timer = playTime;
		timerObj.SetActive ( true );
		start.SetActive ( false );
		finish.SetActive ( false );
		foreach (var c in countNum) {
			c.SetActive ( false );
		}
		pause.SetActive ( false );
		result.SetActive ( false );
		StartCoroutine ( StartLogo () );
		isPause = false;
		isStart = false;
		isEnd = false;

		foreach (var p in GameSetting.Instance.Players) {
			p.GetComponent<Player> ().Gc = this;
			p.GetComponent<Player> ().ShowScore = GameObject.Find ( "ScoreMng" ).GetComponent<ShowScore> ();
		}

		// 星の生成
		GameObject star;
		switch (GameSetting.Instance.Star) {
			case GameSetting.Stars.Jimejime:
				star = (GameObject)Resources.Load ( "Prefabs/St1_Jimejime" );
				break;
			case GameSetting.Stars.Moon:
				star = (GameObject)Resources.Load ( "Prefabs/St2_Moon" );
				break;
			case GameSetting.Stars.Magmag:
				star = (GameObject)Resources.Load ( "Prefabs/St3_Magmag" );
				break;
			default:
				star = (GameObject)Resources.Load ( "Prefabs/St1_Jimejime" );
				break;
		}
		star = Instantiate ( star, new Vector3 ( 0, 0, 0 ), Quaternion.identity );

		// プレイヤー設定
		for (int i = 0; i < GameSetting.Instance.Players.ToArray ().Length; i++) {
			var n = GameSetting.Instance.Players[i].GetComponent<PlayerInput> ().playerIndex;
			playerUIList[n].GetComponent<StatusUI> ().PlayerData = GameSetting.Instance.Players[i].GetComponent<Player> ();
			playerUIList[n].SetActive ( true );
			GameSetting.Instance.Players[i].GetComponent<Player> ().Planet = star;
			GameSetting.Instance.Players[i].transform.position = new Vector3 ( i * -2, 3, 3 );
			GameSetting.Instance.Players[i].transform.Rotate ( new Vector3 ( 40, 0, 0 ) );
			GameSetting.Instance.Players[i].GetComponent<Player> ().Reset ();
		}

		foreach (GameObject o in GameSetting.Instance.Players) {
			Rank.Add ( o.GetComponent<Player> () );
		}

		SoundManager.Instance.PlayBGM ( SoundManager.BGM.MainGame );
	}

	// Update is called once per frame
	void Update () {
		// 順位計算
		Rank = Rank.OrderBy ( x => x.Score ).ToList ();
		Rank.Reverse ();

		// タイマー
		if (isStart == true && isEnd == false && isPause == false) {
			timer -= Time.deltaTime;
			var t = (int)timer;
			for (int d = 0; d < timers.Length; d++) {
				var n = t % 10;
				timers[d].texture = numbers[n];
				t /= 10;
			}
		}

		if (timer < 0) {
			isEnd = true;
		}

		if (isEnd == true && finish.activeSelf == false && result.activeSelf == false) {    // ゲーム終了したら
			SoundManager.Instance.PlaySE ( SoundManager.SE.Finish );
			finish.SetActive ( true );
			// 1秒後にもろもろ表示する
			Invoke ( "EndProc", 1.0f );
		}
	}

	void EndProc () {
		SoundManager.Instance.PlayBGM ( SoundManager.BGM.Result );
		timerObj.SetActive ( false );
		finish.SetActive ( false );
		result.SetActive ( true );
	}

	public void Pause ( bool value ) {
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
}
