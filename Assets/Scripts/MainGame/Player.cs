using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
	Rigidbody rb;
	GameControl gc;
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
	float slowSpeed;
	float changeSpeedTimer; // 元のスピードに戻る
	const float GRAVITY = 50;
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
	float invincibleTimer; // 無敵タイマー
	readonly float invincibleInterval = 1.0f;   // 無敵
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
	ShowScore showScore;
	public ShowScore ShowScore { get => showScore; set => showScore = value; }

	// アイテム
	bool isHitItemBox;
	public bool IsHitItemBox {
		get { return isHitItemBox; }
		set { isHitItemBox = value; }
	}
	bool usableRadar;
	public bool UsableRadar {
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

	Animator anim;

	// 入力
	Vector2 axis;
	float shotPressTime;
	bool shotPressed;
	readonly float timeToAim = 0.2f;
	bool canMove = true;

	// エフェクト
	GameObject muzzleEffect;

	void Start () {
		bc = GetComponent<BoxCollider> ();
		mr = GetComponent<MeshRenderer> ();
		rb = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		fieldBullets = new Queue<GameObject> ();
		speed = baseSpeed;
		slowSpeed = baseSpeed * 0.7f;
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
		invincibleTimer = invincibleInterval;
		muzzleEffect = (GameObject)Resources.Load ( "Prefabs/Effects/Muzzle/MuzzleFlash" );
	}

	void Update () {
		if (planet == null) return;

		// Movement
		if (canMove == true) {
			float x = 0;
			float z = 0;
			if (disableInput == false) {
				x = axis.x * Time.deltaTime * speed;
				z = axis.y * Time.deltaTime * -speed;
			}

			transform.Translate ( x, 0, z );
		}

		// Rotate
		if (disableInput == false && axis != Vector2.zero) {
			float y = axis.x * 150 * Time.deltaTime;
			transform.Rotate ( 0, y, 0 );
		}

		// Gravity
		RaycastHit hit = new RaycastHit ();
		if (Physics.Raycast ( transform.position, -transform.up, out hit, 10 )) {
			if (hit.collider.isTrigger == false && hit.collider.tag != "Wall") {
				distanceToGround = hit.distance;
				groundNormal = hit.normal;
			}

			onGround = (distanceToGround <= 0.1f) ? true : false;
		}

		Vector3 gravDirection = (transform.position - planet.transform.position).normalized;

		if (onGround == false) {
			rb.AddForce ( gravDirection * -GRAVITY );
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

		// スピードを元に戻す
		changeSpeedTimer += Time.deltaTime;
		if (changeSpeedTimer > 0.3f) {
			speed = baseSpeed;
			anim.SetBool ( "isSlow", false );
		}

		// 無敵時間
		if (disableInput == false) {
			invincibleTimer -= Time.deltaTime;
			if (invincibleTimer < 0.0f) {
				takeDamage = true;
			}
			else {
				takeDamage = false;
			}
		}

		// 構える関係
		if (shotPressed == true) {
			shotPressTime += Time.deltaTime;
		}

		if (shotPressTime >= timeToAim) {
			canMove = false;
			anim.SetBool ( "isAim", true );
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

	public void ResetInvincibleTimer () => invincibleTimer = invincibleInterval;

	private void OnCollisionEnter ( Collision collision ) {
		if (collision.gameObject.tag == "Item") {
			if ((usableRadar == true) || (usableSpdBullet == true) || (usableSpdBullet == true) ||
				(usableBoots == true) || (usableSlowTimer == true)) return;
			isHitItemBox = true;
			SoundManager.Instance.PlaySE ( SoundManager.SE.GetItem );
		}

		if (collision.gameObject.tag == "Satellite" || collision.gameObject.tag == "Bullet") {
			if (takeDamage == true) {
				if (collision.gameObject.tag == "Satellite") {
					score--;
					ShowScore?.Exec ( transform, -1 );
					ResetInvincibleTimer ();
				}

				anim.SetTrigger ( "takeDamage" );
			}
		}
	}

	void OnTriggerEnter ( Collider collider ) {
		if (collider.tag == "Mud") {
			SoundManager.Instance.PlaySE ( SoundManager.SE.Mud );
		}
		else if (collider.tag == "Ash") {
			SoundManager.Instance.PlaySE ( SoundManager.SE.Ash );
		}

		if (collider.tag == "Bush") {
			SoundManager.Instance.PlaySE ( SoundManager.SE.OnBush );
		}
	}

	void OnTriggerStay ( Collider collider ) {
		var t = collider.tag;
		if (t == "Mud" || t == "Ash") {
			speed = slowSpeed;
			anim.SetBool ( "isSlow", true );
			changeSpeedTimer = 0;
		}
	}

	// ------------入力-----------------
	public void OnMove ( InputValue value ) {
		axis = value.Get<Vector2> ();
		axis.y *= -1;
		var b = axis != Vector2.zero;
		anim.SetBool ( "isRunning", b );
	}

	public void OnShot ( InputValue value ) {
		var input = value.Get<float> ();
		// 押した時
		if (input == 1) {
			shotPressed = true;

			if (CanBeShot () == false) return;

			Shot ();
			anim.SetTrigger ( "isShot" );
		}
		// 離したとき
		else {
			shotPressed = false;
			anim.SetBool ( "isAim", false );

			if (shotPressTime >= timeToAim && CanBeShot () == true) Shot ();

			shotPressTime = 0;
			canMove = true;
		}
	}

	bool CanBeShot () {
		return disableInput == false && magazine > 0 && shotCooldown <= 0;
	}

	void Shot () {
		Vector3 shotPos = transform.position + transform.forward * 0.7f;
		GameObject b = Instantiate ( bullet, shotPos, transform.rotation );
		b.GetComponent<Bullet> ().Master = this;
		fieldBullets.Enqueue ( b );
		if (fieldBullets.Count > maxFieldBullet) {
			Destroy ( fieldBullets.Dequeue () );
		}

		SoundManager.Instance.PlaySE ( SoundManager.SE.Shot );
		var e = Instantiate ( muzzleEffect, transform.position, transform.rotation );
		Destroy ( e, 1.0f );

		magazine--;
		onReload = false;
		shotCooldown = shotInterval;
	}

	public void OnPause () {
		GameControl.Instance.Pause ( true );
	}

	public void OnItem ( InputValue value ) {
		if (value.Get<float> () == 1) {
			if (disableInput == true) return;
			if (usableRadar == true) {
				usableRadar = false;
				isStartedRadar = true;
			}
			else if (usableSpdBullet == true) {
				Vector3 shotPos = transform.position + transform.forward * 0.5f;
				GameObject b = Instantiate ( speedBullet, shotPos, transform.rotation );
				b.GetComponent<Bullet> ().Master = this;
				fieldBullets.Enqueue ( b );
				usableSpdBullet = false;
				SoundManager.Instance.PlaySE ( SoundManager.SE.SpeedBullet );
			}
			else if (usableHevBullet == true) {
				Vector3 shotPos = transform.position + transform.forward * 0.5f;
				GameObject b = Instantiate ( heavyBullet, shotPos, transform.rotation );
				b.GetComponent<Bullet> ().Master = this;
				fieldBullets.Enqueue ( b );
				usableHevBullet = false;
				SoundManager.Instance.PlaySE ( SoundManager.SE.HeavyBullet );
			}
			else if (usableBoots == true) {
				usableBoots = false;
				isSpeedUp = true;
			}
			else if (usableSlowTimer == true) {
				usableSlowTimer = false;
				isStartedSlowTimer = true;
			}

			if (disableInput == true || usableBoots == true || usableSlowTimer == true) {
				SoundManager.Instance.PlaySE ( SoundManager.SE.UseItem );
			}
		}
	}

	public void OnBack () {
		GameControl.Instance.Pause ( false );
	}
}
