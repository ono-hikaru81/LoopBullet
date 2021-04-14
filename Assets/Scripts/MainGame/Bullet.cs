using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    GameObject planet;
    public GameObject Planet {
        get { return planet; }
        set { planet = value; }
    }

    public float speed;

    float gravity;
    bool onGround;

    float distanceToGround;
    Vector3 groundNormal;

    Rigidbody rb;

    Player master;
    public Player Master {
        get { return master; }
        set { master = value; }
    }

    void Start () {
        gravity = 100;
        onGround = false;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        planet = master.Planet;
    }

    void Update () {
        // Movement
        float z = Time.deltaTime * speed;

        transform.Translate( 0, 0, z );

        // Gravity
        RaycastHit hit = new RaycastHit();
        if ( Physics.Raycast( transform.position, -transform.up, out hit, 10 ) ) {
            distanceToGround = hit.distance;
            groundNormal = hit.normal;

            onGround = ( distanceToGround <= 0.1f ) ? true : false;
        }

        Vector3 gravDirection = ( transform.position - planet.transform.position ).normalized;

        if ( onGround == false ) {
            rb.AddForce( gravDirection * -gravity );
        }

        // Rotation

        // 惑星に向けて平行をとる
        Quaternion toRotation = Quaternion.FromToRotation( transform.up, groundNormal ) * transform.rotation;
        transform.rotation = toRotation;
    }

    private void OnCollisionEnter ( Collision collision ) {
        if ( collision.gameObject.tag == "Player" ) {
            Player p = collision.gameObject.GetComponent<Player>();
            if ( p.TakeDamage == true ) {
                p.Hp--;
                if ( p.Hp <= 0 ) {
                    master.KilledPlayers.Enqueue( p.icon );
                    p.DeathProc();
                }
            }

            Destroy( gameObject );
        }

        if(collision.gameObject.tag == "Bullet" ) {
            speed *= -1;
        }
    }
}