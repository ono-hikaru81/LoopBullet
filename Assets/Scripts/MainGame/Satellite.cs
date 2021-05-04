using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Satellite : MonoBehaviour {
	Rigidbody rb;
	readonly int GRAVITY = 100;
	[SerializeField] float speed;
	float distanceToGround;
	Vector3 groundNormal;
	bool onGround;
	[SerializeField] GameObject planet;

	// Start is called before the first frame update
	void Start () {
		rb = GetComponent<Rigidbody> ();
		planet = GameObject.Find ( "St3_Magmag(Clone)" );
	}

	// Update is called once per frame
	void Update () {
		// Movement
		float z = Time.deltaTime * speed;

		transform.Translate ( 0, 0, z );

		// Gravity
		RaycastHit hit = new RaycastHit ();
		if (Physics.Raycast ( transform.position, -transform.up, out hit, 10 )) {
			if (hit.collider.isTrigger == false && hit.collider.tag != "Wall") {
				distanceToGround = hit.distance;
				groundNormal = hit.normal;
			}

			onGround = (distanceToGround <= 0.3f) ? true : false;
		}

		Vector3 gravDirection = (transform.position - planet.transform.position).normalized;

		if (onGround == false) {
			rb.AddForce ( gravDirection * -GRAVITY );
		}

		// Rotation

		// 惑星に向けて平行をとる
		Quaternion toRotation = Quaternion.FromToRotation ( transform.up, groundNormal ) * transform.rotation;
		transform.rotation = toRotation;
	}
}
