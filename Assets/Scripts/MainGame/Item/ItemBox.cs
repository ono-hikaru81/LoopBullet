using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    [SerializeField]
    GameObject planet;  // d—Í‚ðŽó‚¯‚é¯
    public GameObject Planet { get => planet; set => planet = value; }
    GameObject gc;
    const float GRAVITY = 100;
    bool onGround;
    float distanceToGround;
    Vector3 groundNormal;
    Rigidbody rb;

    private bool isExit;
    public bool IsExit {
        get { return isExit; }
        set { isExit = value; }
    }

    void Start() {
        gc = GameObject.Find("GameControl");
        onGround = false;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update() {
        // Gravity
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, -transform.up, out hit, 10)) {
            distanceToGround = hit.distance;
            groundNormal = hit.normal;

            onGround = (distanceToGround <= 0.1f) ? true : false;
        }

        Vector3 gravDirection = (transform.position - Planet.transform.position).normalized;

        if (onGround == false) {
            rb.AddForce(gravDirection * -GRAVITY);
        }

        Quaternion toRotation = Quaternion.FromToRotation(transform.up, groundNormal) * transform.rotation;
        transform.rotation = toRotation;
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Player") {
            gc.GetComponent<CreateItemBox>().IsExitItemBox = false;
            Destroy(gameObject);
        }
    }
}
