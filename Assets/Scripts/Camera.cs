using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject player;
    public GameObject planet;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update () {

        // Position
        transform.position = Vector3.Lerp( transform.position, player.transform.position, 0.05f );

        // Rotation
        transform.rotation = player.transform.rotation;
    }
}
