using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
	Rigidbody rb;
	GameControl gc;
	public GameControl Gc { get => gc; set => gc = value; }
	BoxCollider bc;
	MeshRenderer mr;
	GameObject planet;  // 重力を受ける星
	public GameObject Planet {
		get { return planet; }
		set { planet = value; }
	}
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
	float distanceToGround; // 地面との距離
	Vector3 groundNormal;
	[SerializeField] int maxMagazine;   // 最大の弾保持数
	int magazine;   // 現在の弾所持数
	public int Magazine {
		get { return magazine; }
		set { magazine = value; }
	}
	[SerializeField] GameObject bullet; // 撃ち出す弾
	[SerializeField] GameObject speedBullet;
	[SerializeField] GameObject heavyBullet;
	Queue<GameObject> fieldBullets;
	[SerializeField] int maxFieldBullet;    // フィールドに存在できる弾の最大数
	[SerializeField] float shotInterval;  // 射撃間隔
	float shotCooldown;
	bool onReload;  // リロード中かのフラグ
	float reloadTimer;
	[SerializeField] float reloadInterval;  // リロードの間隔
	bool disableInput;  // 入力を受け付けないか
	public bool DisableInput {
		get { return disableInput; }
		set { disableInput = value; }
	}
	bool takeDamage;    // ダメージを受けるか
	public bool TakeDamage {
		get { return takeDamage; }
		set { takeDamage = value; }
	}
	int score;
	public int Score {
		get { return score; }
		set { score = value; }
	}

	// アイテム
	bool isHitItemBox;
	public bool IsHitItemBox {
		get { return isHitItemBox; }
		set { isHitItemBox = value; }
	}
	bool usableRadar;
	public bool UsableRadar
	{
		get { return usableRadar; }
		set { usableRadar = value; }
	}
	bool usableSpdBullet;
	public bool UsableSpdBullet {
		get { return usableSpdBullet; }
		set { usableSpdBullet = value; }
	}
	bool usableHevBullet;
	public bool UsableHevBullet {
		get { return usableHevBullet; }
		set { usableHevBullet = value; }
	}
	bool usableBoots;
	public bool UsableBoots {
		get { return usableBoots; }
		set { usableBoots = value; }
	}
	bool usableSlowTimer;
	public bool UsableSlowTimer {
		get { return usableSlowTimer; }
		set { usableSlowTimer = value; }
	}
	bool isSpeedUp;
	public bool IsSpeedUp {
		get { return isSpeedUp; }
		set { isSpeedUp = value; }
	}
	bool isStartedSlowTimer;
	public bool IsStartedSlowTimer {
		get { return isStartedSlowTimer; }
		set { isStartedSlowTimer = value; }
	}
	bool isStartedRadar;
	public bool IsStartedRadar {
		get { return isStartedRadar; }
		set { isStartedRadar = value; }
	}

	// 入力
	Vector2 axis;

	void Start () {
		bc = GetComponent<BoxCollider> ();
		mr = GetComponent<MeshRenderer> ();
		rb = GetComponent<Rigidbody> ();
		fieldBullets = new Queue<GameObject> ();
		speed = baseSpeed;
		onGround = false;
		magazine = maxMagazine;
		onReload = false;
		shotCooldown = shotInterval;
		disableInput = false;
		takeDamage = true;
		rb.freezeRotation = true;
		score = 0;
		isHitItemBox = false;
		usableRadar = false;
		usableSpdBullet = false;
		usableHevBullet = false;
		usableBoots = false;
		usableSlowTimer = false;
	}

	void Update () {
		if (planet == null) return;

		// Movement
		float x = 0;
		float z = 0;
		if (disableInput == false) {
			x = axis.x * Time.deltaTime * speed;
			z = axis.y * Time.deltaTime * -speed;
		}

		transform.Translate ( x, 0, z );

		// Gravity
		RaycastHit hit = new RaycastHit ();
		if (Physics.Raycast ( transform.position, -transform.up, out hit, 10 )) {
			distanceToGround = hit.distance;
			groundNormal = hit.normal;

			onGround = (distanceToGround <= 0.2f) ? true : false;
		}

		Vector3 gravDirection = (transform.position - planet.transform.position).normalized;

		if (onGround == false) {
			rb.AddForce ( gravDirection * -GRAVITY );
		}

		// Rotate
		if (disableInput == false) {
			float y = axis.x * 150 * Time.deltaTime;
			transform.Rotate ( 0, y, 0 );
		}

		ParallelToPlanet ();

		// Shot
		shotCooldown -= Time.deltaTime;

		// Reload
		if (onReload == true) {
			reloadTimer += Time.deltaTime;
			if (reloadTimer >= reloadInterval) {
				magazine++;
				reloadTimer = 0;
				if (magazine >= maxMagazine) {
					onReload = false;
				}
			}
		}
		else {
			if (magazine <= 0) {
				onReload = true;
			}
		}
	}

	// 惑星に向けて平行をとる
	public void ParallelToPlanet () {
		Quaternion toRotation = Quaternion.FromToRotation ( transform.up, groundNormal ) * transform.rotation;
		transform.rotation = toRotation;
	}

	public void DeathProc () {
		bc.enabled = false;
		mr.enabled = false;
		disableInput = true;
		takeDamage = false;
		rb.constraints = RigidbodyConstraints.FreezePosition;
	}

	public void Reset () {
		magazine = maxMagazine;
		shotCooldown = shotInterval;
		fieldBullets = new Queue<GameObject> ();
		score = 0;
	}

	// ------------入力-----------------
	public void OnMove ( InputValue value ) {
		axis = value.Get<Vector2> ();
		axis.y *= -1;
	}

	public void OnShot ( InputValue value ) {
		if (value.Get<float> () == 1) {
			if (disableInput == true) return;
			if (magazine <= 0 || shotCooldown > 0) return;

			Vector3 shotPos = transform.position + transform.forward * 0.7f;
			GameObject b = Instantiate ( bullet, shotPos, transform.rotation );
			b.GetComponent<Bullet> ().Master = this;
			fieldBullets.Enqueue ( b );
			if (fieldBullets.Count > maxFieldBullet) {
				Destroy ( fieldBullets.Dequeue () );
			}

			magazine--;
			onReload = false;
			shotCooldown = shotInterval;
		}
	}

	public void OnPause () {
		Gc.Pause ( true );
	}

	public void OnItem ( InputValue value ) {
		if(value.Get<float> () == 1) {
			if (disableInput == true) return;
			if (usableRadar == true) {
				usableRadar = false;
				isStartedRadar = true;
			}
			else if (usableSpdBullet == true) {
				Vector3 shotPos = transform.position + transform.forward * 0.5f;
				GameObject b = Instantiate(speedBullet, shotPos, transform.rotation);
				b.GetComponent<Bullet>().Master = this;
				fieldBullets.Enqueue(b);
				usableSpdBullet = false;
			}
			else if (usableHevBullet == true) {
				Vector3 shotPos = transform.position + transform.forward * 0.5f;
				GameObject b = Instantiate(heavyBullet, shotPos, transform.rotation);
				b.GetComponent<Bullet>().Master = this;
				fieldBullets.Enqueue(b);
				usableHevBullet = false;
			}
			else if (usableBoots == true) {
				usableBoots = false;
				isSpeedUp = true;
			}
			else if (usableSlowTimer == true) {
				usableSlowTimer = false;
				isStartedSlowTimer = true;
			}
		}
	}

	public void OnBack () {
		Gc.Pause ( false );
	}

	private void OnCollisionEnter( Collision collision) {
		if(collision.gameObject.tag == "Item") {
			if ((usableRadar == true) || (usableSpdBullet == true) || (usableSpdBullet == true) ||
				(usableBoots == true) || (usableSlowTimer == true)) return;
			isHitItemBox = true;
		}
	}
}
