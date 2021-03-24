using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public GameObject planet;
    public float speed;

    float gravity;
    bool onGround;

    float distanceToGround;
    Vector3 groundNormal;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start () {
        gravity = 100;
        onGround = false;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        Destroy( gameObject, 5.0f );
    }

    // Update is called once per frame
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
        if( collision.gameObject.tag == "Player" ||
            collision.gameObject.tag == "Bullet" ) {
            Destroy( gameObject );
        }
    }
}