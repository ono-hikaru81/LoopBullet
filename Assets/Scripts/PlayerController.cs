using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // é©ï™é©êg
    public float moveSpeed = 15;
    private Rigidbody rb;
    private Transform mytransform;
    private Vector3 moveDir;

    // íe
    public GameObject bullet;
    public Transform muzzle;
    public float bulletSpeed = 30;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mytransform = transform;
    }

    void Update()
    {
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, -Input.GetAxisRaw("Vertical")).normalized;

        if(Input.GetKeyDown(KeyCode.Z))
        {
            GameObject bullets = Instantiate(bullet) as GameObject;
            Vector3 force;
            force = this.gameObject.transform.forward * bulletSpeed;
            bullets.GetComponent<Rigidbody>().AddForce(force);
            bullets.transform.position = muzzle.position;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + mytransform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);
    }
}
