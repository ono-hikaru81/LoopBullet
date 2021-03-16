using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 15;
    private Rigidbody rb;
    private Transform mytransform;
    private Vector3 moveDir;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mytransform = transform;
    }

    void Update()
    {
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + mytransform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);
    }
}
