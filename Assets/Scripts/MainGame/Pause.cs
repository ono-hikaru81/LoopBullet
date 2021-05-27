using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {
	[SerializeField] YesorNo popup;
	private float inputL;
	private float inputR;

	// Start is called before the first frame update
	void Start () {
		popup.gameObject.SetActive ( false );
	}

	// Update is called once per frame
	void Update () {

	}

	// -------------------入力-------------------
	public void OnSelect ( InputValue value ) {
		var a = value.Get<Vector2> ();
		if (popup.gameObject.activeSelf == true) {
			if (a.x != 0) {
				popup.CurrentSelect += (a.x == -1) ? -1 : 1;
				popup.CurrentSelect = UIFunctions.RevisionValue ( popup.CurrentSelect, 2 );
				SoundManager.Instance.PlaySE ( SoundManager.SE.Cursor );
				popup.UpdateUI ();
			}
		}
	}

	public void OnEnter () {
		if (popup.gameObject.activeSelf == true) {
			// はい
			if (popup.CurrentSelect == 0) {
				SoundManager.Instance.PlaySE ( SoundManager.SE.Next );
				GameControl.Instance.Pause ( false );
				Destroy ( GameControl.Instance.gameObject );
				var o = FindObjectOfType<Crown> ();
				Destroy ( o.gameObject );
				foreach (var p in GameSetting.Instance.Players) {
					Destroy ( p.gameObject );
				}
				GameSetting.Instance.Players = new List<GameObject> ();
				SceneManager.LoadScene ( "Title" );
			}
			// いいえ
			else if (popup.CurrentSelect == 1) {
				SoundManager.Instance.PlaySE ( SoundManager.SE.Back );
				popup.gameObject.SetActive ( false );
			}
		}
	}

	public void OnBack () {
		if (popup.gameObject.activeSelf == true) {
			popup.gameObject.SetActive ( false );
		}
		else {
			GameControl.Instance.Pause ( false );
		}
	}

	public void OnL ( InputValue value ) {
		inputL = value.Get<float> ();
		ShowPopup ();
	}

	public void OnR ( InputValue value ) {
		inputR = value.Get<float> ();
		ShowPopup ();
	}

	private void ShowPopup () {
		if (inputL == 1 && inputR == 1 && popup.gameObject.activeSelf == false) {
			popup.gameObject.SetActive ( true );
		}
	}
}
