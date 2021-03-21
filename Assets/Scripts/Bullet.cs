using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject planet;
    private FauxGravityAttractor attractor;
    private Transform mytransform;
    private Rigidbody rb;

    void Start()
    {
        planet = GameObject.Find("Planet");
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.useGravity = false;
        mytransform = transform;
        attractor = planet.GetComponent<FauxGravityAttractor>();
    }

    void Update()
    {
        attractor.Attract(mytransform);
    }
}
