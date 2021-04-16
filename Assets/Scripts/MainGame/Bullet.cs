using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	GameObject planet;  // 重力を受ける星
	public GameObject Planet {
		get { return planet; }
		set { planet = value; }
	}
	[SerializeField] float speed;
	const float GRAVITY = 100;
	bool onGround;
	float distanceToGround; // 地面からの距離
	Vector3 groundNormal;
	Rigidbody rb;
	Player master;  // 自分を撃ち出したプレイヤー
	public Player Master {
		get { return master; }
		set { master = value; }
	}

	void Start () {
		onGround = false;

		rb = GetComponent<Rigidbody> ();
		rb.freezeRotation = true;

		planet = master.Planet;
	}

	void Update () {
		// Movement
		float z = Time.deltaTime * speed;

		transform.Translate ( 0, 0, z );

		// Gravity
		RaycastHit hit = new RaycastHit ();
		if (Physics.Raycast ( transform.position, -transform.up, out hit, 10 )) {
			distanceToGround = hit.distance;
			groundNormal = hit.normal;

			onGround = (distanceToGround <= 0.1f) ? true : false;
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

	private void OnCollisionEnter ( Collision collision ) {
		if (collision.gameObject.tag == "Player") {
			Player p = collision.gameObject.GetComponent<Player> ();
			if (p.TakeDamage == true) {
				if (p != master) {
					master.Score++;
				}

				p.Score--;
			}

			Destroy ( gameObject );
		}

		// 弾にぶつかると進む向きが反転
		if (collision.gameObject.tag == "Bullet") {
			speed *= -1;
		}
	}
}