using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Crown : MonoBehaviour {
	enum State {
		Stay,
		MakeBig,
		MakeSmall
	};

	State state = State.Stay;

	readonly Vector3 maxSize = new Vector3 ( 1.0f, 1.0f, 1.0f );
	readonly Vector3 minSize = new Vector3 ( 0.1f, 0.1f, 0.1f );
	readonly float amount = 0.05f;
	Transform master;

	// Start is called before the first frame update
	void Start () {

	}

	void Awake () {
		transform.localScale = minSize;
	}

	// Update is called once per frame
	void Update () {
		var s = transform.localScale;
		switch (state) {
			case State.Stay: return;
			case State.MakeBig:
				transform.localScale = new Vector3 ( s.x + amount, s.y + amount, s.z + amount );
				if (transform.localScale.x >= maxSize.x) {
					transform.localScale = maxSize;
					state = State.Stay;
				}
				break;
			case State.MakeSmall:
				transform.localScale = new Vector3 ( s.x - amount, s.y - amount, s.z - amount );
				if (transform.localScale.x <= minSize.x) {
					transform.localScale = minSize;
					transform.SetParent ( master );
					Reset ();
				}
				break;
			default: return;
		}
	}

	public void ChangeMaster ( Transform trans ) {
		if (master != trans) {
			state = State.MakeSmall;
			master = trans;
		}
	}

	public void UnsetMaster () {
		master = null;
	}

	public void Reset () {
		if (master == null) return;

		transform.rotation = master.rotation;
		transform.localPosition = new Vector3 ( 0, 0.6f, 0 );
		state = State.MakeBig;
	}
}
