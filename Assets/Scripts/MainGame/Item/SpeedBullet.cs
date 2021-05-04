using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SpeedBullet : MonoBehaviour {
	GameObject planet;  // �d�͂��󂯂鐯
	public GameObject Planet { get => planet; set => planet = value; }
	float speed;
	public float Speed {
		get { return speed; }
		set { speed = value; }
	}
	[SerializeField] float baseSpeed;
	public float BaseSpeed {
		get { return baseSpeed; }
	}
	const float GRAVITY = 100;
	bool onGround;
	float distanceToGround; // �n�ʂ���̋���
	Vector3 groundNormal;
	Rigidbody rb;
	Player master;  // �����������o�����v���C���[
	public Player Master { get => master; set => master = value; }
	float timer;    // ���܂�Ă���̎���
	public float Timer { get => timer; set => timer = value; }
	ShowScore showScore;

	// �G�t�F�N�g
	GameObject hitEffect;

	void Start() {
		speed = baseSpeed;

		showScore = GameObject.Find("ScoreMng").GetComponent<ShowScore>();
		onGround = false;

		rb = GetComponent<Rigidbody>();
		rb.freezeRotation = true;

		Planet = Master.Planet;

		hitEffect = (GameObject)Resources.Load("Prefabs/Effects/Hit/Hit");
	}

	void Update() {
		timer += Time.deltaTime;

		// Movement
		float z = Time.deltaTime * speed;

		transform.Translate(0, 0, z);

		// Gravity
		RaycastHit hit = new RaycastHit();
		if (Physics.Raycast(transform.position, -transform.up, out hit, 10)) {
			if(hit.collider.isTrigger == false && hit.collider.tag != "Wall") {
				distanceToGround = hit.distance;
				groundNormal = hit.normal;
			}			

			onGround = (distanceToGround <= 0.1f) ? true : false;
		}

		Vector3 gravDirection = (transform.position - Planet.transform.position).normalized;

		if (onGround == false) {
			rb.AddForce(gravDirection * -GRAVITY);
		}

		// Rotation

		// �f���Ɍ����ĕ��s���Ƃ�
		Quaternion toRotation = Quaternion.FromToRotation( transform.up, groundNormal ) * transform.rotation;
		transform.rotation = toRotation;
	}

	private void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Player") {
			Player p = collision.gameObject.GetComponent<Player>();
			if (p.TakeDamage == true) {
				var s = ConvertToScore();
				if (p != Master) {
					master.Score += s;
					showScore.Exec( master.transform, s );
				}

				p.Score -= s;
				p.ResetInvincibleTimer();
				showScore.Exec( p.transform, -s );
			}

			var e = Instantiate( hitEffect, transform.position, transform.rotation );
			Destroy( e, 1.0f );
			Destroy( gameObject );
		}
		else if (collision.gameObject.tag == "Satellite") {
			var e = Instantiate( hitEffect, transform.position, transform.rotation );
			Destroy( e, 1.0f );
			Destroy( gameObject );
		}

		// �e�ɂԂ���Ɛi�ތ��������]
		if (collision.gameObject.tag == "Bullet") {
			speed *= -1;
			var e = Instantiate( hitEffect, transform.position, transform.rotation );
			Destroy( e, 1.0f );
		}
	}

	// �������Ԃɉ����ăX�R�A��Ԃ�
	int ConvertToScore() {
		var t = (int)timer;
		if (t == 7) return 7;
		else if (t <= 3) return 3;
		else if (t <= 8) return 2;
		else return 1;
	}
}
