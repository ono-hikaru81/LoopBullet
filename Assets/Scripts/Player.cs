using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject planet;

    [SerializeField]
    private Transform CenterOfGravity;

    [SerializeField]
    float MoveSpeed = 10.0f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate( new Vector3(0, -3f, 0) );
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate( new Vector3(0, 3f, 0) );
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = transform.position + ( transform.forward * MoveSpeed * Time.fixedDeltaTime );
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = transform.position + ( transform.forward * -MoveSpeed * Time.fixedDeltaTime );
        }

        RaycastHit hit;

        // Transformの真下の地形の法線を調べる
        if (Physics.Raycast( CenterOfGravity.position, -transform.up, out hit, float.PositiveInfinity ))
        {
            Quaternion q = Quaternion.FromToRotation( transform.up, hit.normal );

            // 傾き補正
            transform.rotation *= q;

            if ( hit.distance > 1.0f )
            {
                transform.position = transform.position + ( -transform.up * Physics.gravity.magnitude * Time.fixedDeltaTime );
            }
        }
    }

    private void FixedUpdate()
    {
        //Vector3 dir = planet.transform.position - transform.position;
        //Physics.gravity = dir.normalized * 10.0f;

        //float xSpeed = Input.GetAxis("Horizontal") * MoveSpeed;
        //float zSpeed = Input.GetAxis("Vertical") * -MoveSpeed;

        //rb.velocity = new Vector3( xSpeed, rb.velocity.y, zSpeed );
    }

    void OnCollisionEnter( Collision col)
    {
        Debug.Log( "接触" );
    }

    void OnCollisionStay( Collision col )
    {
        Debug.Log( col.gameObject.name );
    }

    void OnCollisionExit( Collision col )
    {
        Debug.Log( "離れた" );
    }
}
