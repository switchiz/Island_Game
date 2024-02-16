using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Mob_Base : MonoBehaviour
{
    int Mob_MaxHp = 1;

    int Mob_hp;

    int Mob_x, Mob_z; // ������ ���� ��ġ�� ����ϱ� ���� ����

    /// <summary>
    /// �÷��̾� �߰� ( true�� �߰� , false�� �̹߰� )
    /// </summary>
    bool player_checked;

    MapObject tempCell; // ����ִ� ���� ����ϱ� ���� �ӽ� �����

    MapObject[] checkMap; // �� ������Ʈ �о���̱�
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

            Mob_x = tempCell.x;
            Mob_z = tempCell.z;
        }

        checkMap = GameManager.Instance.MapObject;

        player = GameManager.Instance.Player;

        player.Turn_Action += Mob_Action;
    }

    /// <summary>
    /// �÷��̾ �ൿ�Ҷ����� ( �� �ϸ��� ) �ൿ�� �ൿ
    /// </summary>
    private void Mob_Action()
    {
        //move();
        
        if ( !player_checked ) // �÷��̾ �߰����� �������� �ൿ
        {
            randomBlockSelect();
        }

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
            moveSet(objectkey); // �̵���
            objectkey.available_move = false; // �̵��� ���� move�� false�� ����
            Debug.Log($"�̵�{objectkey.x},{objectkey.z}");
            tempCell.available_move = tempCell.available; // ������ ����ִ� �� �ʱ�ȭ
            tempCell = objectkey; // ���� ��� �ִ� ���� tempCell�� ��ϵ�.
        }
        }
    }

    private void randomBlockSelect() // ���� 8ĭ�� 1ĭ�� ����, ���̶�� ��õ�
    {
        int[] moveBlock = new int[8]; // 8ĭ ����
        int ArrayBlock = 0; // �迭�� ������ �� ++ , �ʱ�ȭ


        for (int x = Mob_x - 1; x <= Mob_x + 1; x++) // ���� 8ĭ�� ��
        {
            for (int z = Mob_z - 1; z <= Mob_z + 1; z++)
            {
                if (x == Mob_x && z == Mob_z) continue; // �ڱ� �� �ǳʶٱ�

                for (int i = 0; i < checkMap.Length; i++) //  ��� MapObject Ȯ��
                {
                    if (checkMap[i].x == x && checkMap[i].z == z && checkMap[i].available_move == true) // i ��° MapObject�� ��ǥ�� ���� �̵������̶��,
                    {
                        moveBlock[ArrayBlock] = i; // i ��° ���� ���
                        ArrayBlock++;

                    }
                }
            }
        }

        if (ArrayBlock > 0) // �Ҵ�� ��Ұ� ���� ���� ����
        {
            moveSet(checkMap[moveBlock[Random.Range(0, ArrayBlock)]]);
        }
    }

    void moveSet(MapObject obj)
    {
        Mob_x = obj.x;
        Mob_z = obj.z;
        transform.position = new Vector3(obj.x * 0.4f, obj.height, obj.z * 0.4f);
    }


    private void movePlayer(Ray ray)
    {
    }







}
