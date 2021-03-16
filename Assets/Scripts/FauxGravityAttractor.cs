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

        // ���́@"rb.AddForce(gravityUp * gravity);" ���@"body.ridbody.AddForce(gravityUp * gravity);" �ɒ�����Θf���ɉ����Ĉړ��ł��邩��..?
        // ����Â��o�[�W�����̎��������č�������� "body.ridbody.AddForce(gravityUp * gravity);" �ō��ƃG���[��f���₪��B
        // ������
        // �e�Ɏg���Ȃ猻�󂪍œK������..?   
        rb.AddForce(gravityUp * gravity);
        // body.ridbody.AddForce(gravityUp * gravity);

        Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * body.rotation;
        body.rotation = Quaternion.Slerp(body.rotation, targetRotation, 50 * Time.deltaTime);
    }
}
