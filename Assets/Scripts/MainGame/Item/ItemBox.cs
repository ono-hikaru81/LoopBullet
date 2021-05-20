using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour {
    Rigidbody rb;
    GameObject gc;
    MeshRenderer mr;
    [SerializeField] GameObject planet;  // d—Í‚ðŽó‚¯‚é¯
    public GameObject Planet { get => planet; set => planet = value; }
    const float GRAVITY = 100;
    bool onGround;
    float distanceToGround;
    Vector3 groundNormal;

    bool isExit;
    public bool IsExit {
        get { return isExit; }
        set { isExit = value; }
    }

    int itemNum;
    int randomNum;

    void Start() {
        gc = GameObject.Find("GameControl");
        onGround = false;
        rb = GetComponent<Rigidbody>();
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
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Wall") {
            gc.GetComponent<CreateItemBox>().IsExitItemBox = false;
            gc.GetComponent<ItemManager>().IsAssignedItem = false;
            Destroy(gameObject);
        }
    }
}
