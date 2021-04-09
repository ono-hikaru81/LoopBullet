using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public GameObject planet;
    public float speed;

    public string axisX;
    public string axisY;
    public string shot;

    public int maxHp;
    int hp;
    public int Hp {
        get { return hp; }
        set { hp = value; }
    }

    float gravity;
    bool onGround;

    float distanceToGround;
    Vector3 groundNormal;

    Rigidbody rb;

    public int maxMagazine;
    int magazine;
    public int Magazine {
        get { return magazine; }
        set { magazine = value; }
    }
    public GameObject bullet;
    Queue<GameObject> fieldBullets;
    public int maxFieldBullet;

    public float shotInterval;
    float shotCooldown;

    bool onReload;
    float reloadTimer;
    public float reloadInterval;

    bool disableInput;
    public bool DisableInput {
        get { return disableInput; }
        set { disableInput = value; }
    }

    bool takeDamage;
    public bool TakeDamage {
        get { return takeDamage; }
        set { takeDamage = value; }
    }

    public GameControl gc;

    public Texture icon;
    public Queue<Texture> killedPlayers;

    void Start () {
        killedPlayers = new Queue<Texture>();
        fieldBullets = new Queue<GameObject>();
        gravity = 100;
        onGround = false;
        magazine = maxMagazine;
        hp = maxHp;
        onReload = false;
        disableInput = false;
        shotCooldown = shotInterval;
        takeDamage = true;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update () {
        // Movement
        float x = 0;
        float z = 0;
        if ( disableInput == false ) {
            x = Input.GetAxis( axisX ) * Time.deltaTime * speed;
            z = Input.GetAxis( axisY ) * Time.deltaTime * -speed;
        }

        transform.Translate( x, 0, z );

        // Gravity
        RaycastHit hit = new RaycastHit();
        if ( Physics.Raycast( transform.position, -transform.up, out hit, 10 ) ) {
            distanceToGround = hit.distance;
            groundNormal = hit.normal;

            onGround = ( distanceToGround <= 0.2f ) ? true : false;
        }

        Vector3 gravDirection = ( transform.position - planet.transform.position ).normalized;

        if ( onGround == false ) {
            rb.AddForce( gravDirection * -gravity );
        }

        // Rotate
        if ( disableInput == false ) {
            float y = Input.GetAxis( axisX ) * 150 * Time.deltaTime;
            transform.Rotate( 0, y, 0 );
        }

        // 惑星に向けて平行をとる
        Quaternion toRotation = Quaternion.FromToRotation( transform.up, groundNormal ) * transform.rotation;
        transform.rotation = toRotation;

        // Shot
        shotCooldown -= Time.deltaTime;

        if ( Input.GetButtonDown( shot ) && disableInput == false ) {
            if ( magazine > 0 && shotCooldown <= 0 ) {
                Vector3 shotPos = transform.position + transform.forward * 0.7f;
                GameObject b = Instantiate( bullet, shotPos, transform.rotation );
                b.GetComponent<Bullet>().Master = this;
                fieldBullets.Enqueue( b );
                if ( fieldBullets.Count > maxFieldBullet ) {
                    Destroy( fieldBullets.Dequeue() );
                }

                magazine--;
                onReload = false;
                shotCooldown = shotInterval;
            }
        }

        // Reload
        if ( magazine <= 0 ) {
            onReload = true;
        }

        if ( onReload == true ) {
            reloadTimer += Time.deltaTime;
            if ( reloadTimer >= reloadInterval ) {
                magazine++;
                reloadTimer = 0;
                if ( magazine >= maxMagazine ) {
                    onReload = false;
                }
            }
        }

        // Pause
        if ( Input.GetButtonDown( "Start" ) ) {
            gc.Pause( true );
        }
        else if ( Input.GetButtonDown( "Cancel" ) ){
            gc.Pause( false );
        }
    }
}
