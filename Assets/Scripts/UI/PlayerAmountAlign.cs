using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmountAlign : MonoBehaviour {
	[SerializeField] float translateAmount;

	// Start is called before the first frame update
	void Start () {
		var x = translateAmount * GameSetting.MAX_PLAYER - translateAmount * GameSetting.Instance.Players.Count - 1;
		var p = transform.localPosition;
		p.x += x;
		transform.localPosition = p;
	}

	// Update is called once per frame
	void Update () {

	}
}
