using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	Rigidbody rb;
	[SerializeField] GameControl gc;
	BoxCollider bc;
	MeshRenderer mr;
	GameObject planet;  // 重力を受ける星
	public GameObject Planet {
		get { return planet; }
		set { planet = value; }
	}
	[SerializeField] float speed;
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
	[SerializeField] Texture icon;
	public Texture Icon {
		get { return icon; }
	}
	[SerializeField] Texture winBanner;
	public Texture WinBanner {
		get { return winBanner; }
	}
	GameSetting.PlayerData data;    // プレイヤーデータ
	public GameSetting.PlayerData Data {
		get { return data; }
		set { data = value; }
	}
	int score;
	public int Score {
		get { return score; }
		set { score = value; }
	}

	void Start () {
		bc = GetComponent<BoxCollider> ();
		mr = GetComponent<MeshRenderer> ();
		rb = GetComponent<Rigidbody> ();
		fieldBullets = new Queue<GameObject> ();
		onGround = false;
		magazine = maxMagazine;
		onReload = false;
		shotCooldown = shotInterval;
		disableInput = false;
		takeDamage = true;
		rb.freezeRotation = true;
		if (data.Join == false) {
			DeathProc ();
		}
		score = 0;
	}

	void Update () {
		// Movement
		float x = 0;
		float z = 0;
		if (disableInput == false) {
			x = Input.GetAxis ( data.Horizontal ) * Time.deltaTime * speed;
			z = Input.GetAxis ( data.Vertical ) * Time.deltaTime * -speed;
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
			float y = Input.GetAxis ( data.Horizontal ) * 150 * Time.deltaTime;
			transform.Rotate ( 0, y, 0 );
		}

		// 惑星に向けて平行をとる
		Quaternion toRotation = Quaternion.FromToRotation ( transform.up, groundNormal ) * transform.rotation;
		transform.rotation = toRotation;

		// Shot
		shotCooldown -= Time.deltaTime;

		if (Input.GetButtonDown ( data.Shot ) && disableInput == false) {
			if (magazine > 0 && shotCooldown <= 0) {
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

		// Pause
		if (Input.GetButtonDown ( "Start" )) {
			gc.Pause ( true );
		}
		else if (Input.GetButtonDown ( "Cancel" )) {
			gc.Pause ( false );
		}
	}

	public void DeathProc () {
		bc.enabled = false;
		mr.enabled = false;
		disableInput = true;
		takeDamage = false;
		rb.constraints = RigidbodyConstraints.FreezePosition;
	}
}
