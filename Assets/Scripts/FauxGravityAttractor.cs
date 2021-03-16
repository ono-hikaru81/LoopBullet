using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityAttractor : MonoBehaviour
{
    public float gravity = -10;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    public void Attract(Transform body)
    {
        Vector3 gravityUp = (body.position - transform.position).normalized;
        Vector3 bodyUp = body.up;

        // 下の　"rb.AddForce(gravityUp * gravity);" を　"body.ridbody.AddForce(gravityUp * gravity);" に直せれば惑星に沿って移動できるかも..?
        // 現状古いバージョンの資料を見て作ったから "body.ridbody.AddForce(gravityUp * gravity);" で作るとエラーを吐きやがる。
        // くそが
        // 弾に使うなら現状が最適化かも..?   
        rb.AddForce(gravityUp * gravity);
        // body.ridbody.AddForce(gravityUp * gravity);

        Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * body.rotation;
        body.rotation = Quaternion.Slerp(body.rotation, targetRotation, 50 * Time.deltaTime);
    }
}
