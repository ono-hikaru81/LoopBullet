using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowScore : MonoBehaviour {
	[SerializeField] Texture[] plusTexs;
	[SerializeField] Texture[] minusTexs;
	GameObject plate;

	// Start is called before the first frame update
	void Start () {
		plate = (GameObject)Resources.Load ( "Prefabs/UI/Score" );
	}

	// Update is called once per frame
	void Update () {

	}

	public void Exec ( Transform trans, int score ) {
		var o = Instantiate ( plate, trans.position, trans.rotation );
		var v = score.CompareTo ( 0 );
		Texture t = v switch
		{
			1 => plusTexs[score],
			_ => minusTexs[-score],
		};
		o.GetComponent<MeshRenderer> ().material.mainTexture = t;
		o.transform.position += o.transform.up;
		o.transform.rotation = Quaternion.LookRotation ( -trans.forward, trans.up );
		Destroy ( o, 1.0f );
	}
}
