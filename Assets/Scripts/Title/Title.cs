using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Title : MonoBehaviour {
	// Start is called before the first frame update
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void OnEnter () => TitleManager.ChangeScreen ( TitleManager.Screens.Menu );
}
