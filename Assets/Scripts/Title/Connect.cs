using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Connect : MonoBehaviour {
	[SerializeField] GameSetting gs;
	[SerializeField] GameObject[] icons;
	[SerializeField] GameObject[] unconnecteds;
	GameSetting.PlayerData[] binds;

	// Start is called before the first frame update
	void Start () {
		{
			string h = "Horizontal";
			string v = "Vertical";
			string s = "Shot";
			string i = "Item";
			System.Array.Resize ( ref binds, 4 );
			for (int p = 1; p < 5; p++) {
				string n = p.ToString ();
				binds[p - 1] = new GameSetting.PlayerData () { Join = false, Horizontal = h + n, Vertical = v + n, Shot = s + n, Item = i + n };
			}
		}

		for (int i = 0; i < gs.ButtonBinds.Length; i++) {
			if (gs.ButtonBinds[i].Join == true) {
				icons[i].SetActive ( true );
				unconnecteds[i].SetActive ( false );
			}
			else {
				icons[i].SetActive ( false );
				unconnecteds[i].SetActive ( true );
			}

			if (gs.GetJoinedPlayers () == 0) {
				gs.ButtonBinds[i] = binds[i];
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ( "Connect1" )) {
			Entry ( 0 );
		}

		if (Input.GetButtonDown ( "Connect2" )) {
			Entry ( 1 );
		}

		if (Input.GetButtonDown ( "Connect3" )) {
			Entry ( 2 );
		}

		if (Input.GetButtonDown ( "Connect4" )) {
			Entry ( 3 );
		}
	}

	void Entry ( int num ) {
		bool j = gs.ButtonBinds[num].Join;
		gs.ButtonBinds[num].Join = !j;
		icons[num].SetActive ( !j );
		unconnecteds[num].SetActive ( j );
	}
}
