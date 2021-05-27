using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class YesorNo : MonoBehaviour {
	[SerializeField] UIShadow yes;
	[SerializeField] UIShadow no;
	private int currentSelect;
	public int CurrentSelect { get => currentSelect; set => currentSelect = value; }

	// Start is called before the first frame update
	void Start () {
		UpdateUI ();
	}

	// Update is called once per frame
	void Update () {

	}

	public void UpdateUI () {
		switch (currentSelect) {
			case 0:
				yes.SetBrightMode ( UIShadow.BrightMode.Bright );
				no.SetBrightMode ( UIShadow.BrightMode.Shadow );
				break;
			case 1:
				yes.SetBrightMode ( UIShadow.BrightMode.Shadow );
				no.SetBrightMode ( UIShadow.BrightMode.Bright );
				break;
		}
	}
}
