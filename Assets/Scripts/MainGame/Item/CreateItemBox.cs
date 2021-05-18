using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateItemBox : MonoBehaviour {
    GameObject item;
    [SerializeField] GameObject radar;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject boots;
    [SerializeField] GameObject slowTimer;
    GameObject gc;

    bool isExitItemBox;
    public bool IsExitItemBox {
        get { return isExitItemBox; }
        set { isExitItemBox = value; }
    }
    float timer;
    [SerializeField] float intervalTime;
    int randNum;

    void Start() {
        item = null;
        gc = GameObject.Find("GameControl");
        isExitItemBox = false;
        timer = 0.0f;
        randNum = -1;
    }

    void Update() {
        if (gc.GetComponent<ItemManager>().IsAssignedItem == true) {
            if (gc.GetComponent<ItemManager>().currentState == ItemManager.Item.radar) {
                item = radar;
            }
            else if (gc.GetComponent<ItemManager>().currentState == ItemManager.Item.speedBullet ||
                     gc.GetComponent<ItemManager>().currentState == ItemManager.Item.heavyBullet) {
                item = bullet;
            }
            else if (gc.GetComponent<ItemManager>().currentState == ItemManager.Item.boots) {
                item = boots;
            }
            else if (gc.GetComponent<ItemManager>().currentState == ItemManager.Item.slowTimer) {
                item = slowTimer;
            }
        }

        if (isExitItemBox == false) {
            timer += Time.deltaTime;            
            if (timer >= intervalTime) {
                randNum = Random.Range(0, 6);

                if (item != null) {
                    switch (randNum) {
                        case 0:
                            Instantiate(item, new Vector3(2.85f, 0.0f, 0.0f), Quaternion.identity);
                            break;
                        case 1:
                            Instantiate(item, new Vector3(-2.85f, 0.0f, 0.0f), Quaternion.identity);
                            break;
                        case 2:
                            Instantiate(item, new Vector3(0.0f, 2.85f, 0.0f), Quaternion.identity);
                            break;
                        case 3:
                            Instantiate(item, new Vector3(0.0f, -2.85f, 0.0f), Quaternion.identity);
                            break;
                        case 4:
                            Instantiate(item, new Vector3(0.0f, 0.0f, 2.85f), Quaternion.identity);
                            break;
                        case 5:
                            Instantiate(item, new Vector3(0.0f, 0.0f, -2.85f), Quaternion.identity);
                            break;
                    }
                }
                isExitItemBox = true;
                timer = 0.0f;
                randNum = -1;
            }
        }
        Debug.Log(gc.GetComponent<ItemManager>().currentState);
        Debug.Log(item);
    }
}
