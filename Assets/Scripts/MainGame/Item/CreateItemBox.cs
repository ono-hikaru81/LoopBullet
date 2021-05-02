using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateItemBox : MonoBehaviour
{
    [SerializeField] GameObject itemBox;

    bool isExitItemBox;
    public bool IsExitItemBox {
        get { return isExitItemBox; }
        set { isExitItemBox = value; }
    }
    private float timer;
    [SerializeField] private float intervalTime;
    int randNum;

    void Start() {
        isExitItemBox = false;
        timer = 0.0f;
        randNum = -1;
    }

    void Update() {
        if (isExitItemBox == false) {
            timer += Time.deltaTime;
            randNum = Random.Range(0, 6);
            if (timer >= intervalTime) {
                switch (randNum) {
                    case 0:
                        Instantiate(itemBox, new Vector3(2.85f, 0.0f, 0.0f), Quaternion.identity);
                        break;
                    case 1:
                        Instantiate(itemBox, new Vector3(-2.85f, 0.0f, 0.0f), Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(itemBox, new Vector3(0.0f, 2.85f, 0.0f), Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(itemBox, new Vector3(0.0f, -2.85f, 0.0f), Quaternion.identity);
                        break;
                    case 4:
                        Instantiate(itemBox, new Vector3(0.0f, 0.0f, 2.85f), Quaternion.identity);
                        break;
                    case 5:
                        Instantiate(itemBox, new Vector3(0.0f, 0.0f, -2.85f), Quaternion.identity);
                        break;
                }
                isExitItemBox = true;
                timer = 0.0f;
                randNum = -1;
            }
        }
    }
}
