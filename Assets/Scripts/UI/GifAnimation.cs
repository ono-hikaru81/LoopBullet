using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GifAnimation : MonoBehaviour {
	RawImage rawImage;
	[SerializeField] Texture[] textures;
	[SerializeField] float interval;
	private float timer;
	private int currentNum;

	// Start is called before the first frame update
	void Start () {
		rawImage = GetComponent<RawImage> ();
		timer = interval;
		rawImage.texture = textures[currentNum];
	}

	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;

		if (timer < 0) {
			currentNum++;
			currentNum = UIFunctions.RevisionValue ( currentNum, textures.Length - 1, UIFunctions.RevisionMode.Loop );
			rawImage.texture = textures[currentNum];
			timer = interval;
		}
	}
}
