using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject planet;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate () {
        Vector3 dir = planet.transform.position - transform.position;
        Physics.gravity = dir.normalized * 10.0f;


        float xSpeed = Input.GetAxis( "Horizontal" ) * 10.0f;
        float ySpeed = Input.GetAxis( "Vertical" ) * -10.0f;

        rb.velocity = new Vector3( xSpeed, rb.velocity.y, ySpeed );
    }
}
