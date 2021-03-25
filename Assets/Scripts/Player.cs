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

    Rigidbody rb;
    BoxCollider bc;

    public GameObject bullet;

    void Start()
    {
        gravity = 100;
        onGround = false;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        bc = GetComponent<BoxCollider>();
    }

    void Update()
    {
        // Movement
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * -speed;

        transform.Translate(x, 0, z);

        // Gravity
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, -transform.up, out hit, 10))
        {
            distanceToGround = hit.distance;
            groundNormal = hit.normal;

            onGround = (distanceToGround <= 0.2f) ? true : false;
        }

        Vector3 gravDirection = (transform.position - planet.transform.position).normalized;

        if (onGround == false)
        {
            rb.AddForce(gravDirection * -gravity);
        }

        // ˜f¯‚ÉŒü‚¯‚Ä•½s‚ð‚Æ‚é
        Quaternion toRotation = Quaternion.FromToRotation(transform.up, groundNormal) * transform.rotation;
        transform.rotation = toRotation;

        // Shot
        if (Input.GetButtonDown("Shot"))
        {
            Vector3 shotPos = transform.position + transform.forward * 0.5f;

            Instantiate(bullet, shotPos, transform.rotation);
        }
    }
}
