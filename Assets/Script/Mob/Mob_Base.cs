using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_Base : MonoBehaviour
{
    int Mob_MaxHp = 1;

    int Mob_hp;

    MapObject tempCell; // 밟고있던 땅을 기억하기 위한 임시 저장고

    private void Awake()
    {
        Mob_hp = Mob_MaxHp;
    }






    private void Start()
    {
        Ray ray = new Ray(transform.position, Vector3.down); // 시작했을때 null을 피하기위해 시작 땅을 기록함
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 1.0f))
        {
            GameObject selectObj = hitInfo.collider.gameObject;
            tempCell = selectObj.gameObject.GetComponent<MapObject>();
            tempCell.available_move = false;
        }
    }

    private void movePlayer(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 10.0f)) // 이동
        {
            GameObject selectObj = hitInfo.collider.gameObject;
            MapObject objectkey = selectObj.gameObject.GetComponent<MapObject>();

            if (objectkey.available_move && !(tempCell.x == objectkey.x && tempCell.z == objectkey.z))
            {
                moveSet(objectkey.x, objectkey.height, objectkey.z); // 이동함
                objectkey.available_move = false; // 이동한 땅의 move를 false로 만듬
                Debug.Log($"이동{objectkey.x},{objectkey.z}");
                tempCell.available_move = tempCell.available; // 밟고있던 땅 초기화
                tempCell = objectkey; // 이동전 밟고 있던 땅이 기록됨.
            }
        }
    }

    void moveSet(float x, float y, float z)
    {
        x *= 0.4f;
        z *= 0.4f;
        transform.position = new Vector3(x, y, z);
    }






}
