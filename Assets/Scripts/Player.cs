using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject planet;
    public float speed;

    float gravity;
    bool onGround;

    float distanceToGround;
    Vector3 groundNormal;

    Vector3 latestPos;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        gravity = 100;
        onGround = false;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        float x = Input.GetAxis( "Horizontal" ) * Time.deltaTime * speed;
        float z = Input.GetAxis( "Vertical" ) * Time.deltaTime * speed;

        transform.Translate( x, 0, z );

        // Gravity
        RaycastHit hit = new RaycastHit();
        if( Physics.Raycast(transform.position, -transform.up, out hit, 10 ) ) {
            distanceToGround = hit.distance;
            groundNormal = hit.normal;

            onGround = ( distanceToGround <= 0.2f ) ? true : false;
        }

        Vector3 gravDirection = ( transform.position - planet.transform.position ).normalized;

        if( onGround == false ) {
            rb.AddForce( gravDirection * -gravity );
        }

        // Rotation
        if( Input.GetButton( "CamRight" ) ) {
            transform.Rotate( 0, 150 * Time.deltaTime, 0 );
        }
        if( Input.GetButton( "CamLeft" ) ) {
            transform.Rotate( 0, -150 * Time.deltaTime, 0 );
        }

        // 惑星に向けて平行をとる
        Quaternion toRotation = Quaternion.FromToRotation( transform.up, groundNormal ) * transform.rotation;
        transform.rotation = toRotation;
    }
}
