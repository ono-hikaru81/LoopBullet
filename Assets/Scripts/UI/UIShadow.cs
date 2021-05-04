using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class UIShadow : MonoBehaviour {
	public enum BrightMode {
		Bright,
		Shadow,
	};

	[SerializeField] Texture baseTex;
	[SerializeField] Texture shadowTex;
	[SerializeField] RawImage rawImage;

	// Start is called before the first frame update
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void SetBrightMode ( BrightMode mode ) {
		rawImage.texture = mode switch
		{
			BrightMode.Bright => baseTex,
			BrightMode.Shadow => shadowTex,
			_ => baseTex,
		};
	}
}
