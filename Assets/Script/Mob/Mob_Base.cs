using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_Base : MonoBehaviour
{
    int Mob_MaxHp = 1;

    int Mob_hp;

    MapObject tempCell; // ����ִ� ���� ����ϱ� ���� �ӽ� �����
    Player player;

    private void Awake()
    {
        Mob_hp = Mob_MaxHp;
    }

    private void Start()
    {
        Ray ray = new Ray(transform.position, Vector3.down); // ���������� null�� ���ϱ����� ���� ���� �����
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 1.0f))
        {
            GameObject selectObj = hitInfo.collider.gameObject;
            tempCell = selectObj.gameObject.GetComponent<MapObject>();
            tempCell.available_move = false;
        }

        player = GameManager.Instance.Player;
        player.Turn_Action += Mob_Action;
    }

    /// <summary>
    /// �÷��̾ �ൿ�Ҷ����� ( �� �ϸ��� ) �ൿ�� �ൿ
    /// </summary>
    private void Mob_Action()
    {
        //move();
    }

    private void move()
    {
        Ray ray = new Ray (transform.position, Vector3.up); // �ӽ� �ƹ����Գ� ����

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 10.0f)) // �̵�
        {
            GameObject selectObj = hitInfo.collider.gameObject;
        MapObject objectkey = selectObj.gameObject.GetComponent<MapObject>();

        if (objectkey.available_move && !(tempCell.x == objectkey.x && tempCell.z == objectkey.z))
        {
            moveSet(objectkey.x, objectkey.height, objectkey.z); // �̵���
            objectkey.available_move = false; // �̵��� ���� move�� false�� ����
            Debug.Log($"�̵�{objectkey.x},{objectkey.z}");
            tempCell.available_move = tempCell.available; // ����ִ� �� �ʱ�ȭ
            tempCell = objectkey; // �̵��� ��� �ִ� ���� ��ϵ�.
        }
        }
    }

    private void movePlayer(Ray ray)
    {
      

       
    }

    void moveSet(float x, float y, float z)
    {
        x *= 0.4f;
        z *= 0.4f;
        transform.position = new Vector3(x, y, z);
    }






}
