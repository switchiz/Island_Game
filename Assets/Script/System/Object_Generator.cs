using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Object_Generator : MonoBehaviour
{
    Player player;
    Grid_System grid_System;
    MapObject[] checkMap;

    MapObject createMap;

    public GameObject treasure; // ��������
    public GameObject rare_treasure; // ���_��������
    public GameObject Lion;     // ���� ( ���� )
    public GameObject chick;     // ���� ( ���� )

    int Turn_Count;

    private void Awake()
    {
        player = GameManager.Instance.Player;
        grid_System = GameManager.Instance.Grid;
        checkMap = GameManager.Instance.MapObject;

        player.Turn_Action += Object_Count;
    }

    void Object_Count()
    {
        Turn_Count++;
        if ( Turn_Count == 1)
        {
            int i = Random.Range(1, 11); // 0~10
            if (i < 2) CreateObject(rare_treasure); // 1 �϶��� 
            else CreateObject(treasure);              // 3~10 

            CreateObject(Lion);
            CreateObject(Lion);
        }

        if ( Turn_Count%20 == 0) // 20�� �ֱ�� �ڽ� ���� 10% Ȯ���� ���� ��������
        {
            int i = Random.Range(1, 11); // 0~10

            if ( i < 2 ) CreateObject(rare_treasure); // 1 �϶��� 
            else CreateObject(treasure);              // 3~10 
        }

        if ( Turn_Count%32 == 0) // 32�� �ֱ�� ����
        {
            CreateObject(Lion);
        }

        if (Turn_Count % 15 == 0) // 15�� �ֱ�� ��
        {
            CreateObject(chick);
        }



        if ( Turn_Count > 120 )
        {
            Turn_Count = 0;
        }

    }

    private void CreateObject(GameObject obj)
    {
        int[] moveBlock = new int[checkMap.Length];
        int temp = 0;

        for (int i = 0; i < checkMap.Length; i++) //  ��� MapObject Ȯ��
        {
            if (checkMap[i].Available_move == true) // i ��° MapObject�� �̵������̶��,
            {
                moveBlock[temp] = i; // i ��° ���� ���
                temp++;
            }
        }
        createMap = checkMap[moveBlock[Random.Range(0, temp)]];     // ������ ���� �Ҵ�

        Vector3 objPos = new Vector3(createMap.x * 0.4f, createMap.height,createMap.z * 0.4f);
        Quaternion creationRotation = Quaternion.identity;

        GameObject CreateObj = Instantiate(obj,objPos,creationRotation); // �ش�Ǵ� �ν��Ͻ� ����
        


        // �ش�Ǵ� �ν��Ͻ����� ������Ʈ �ҷ����� ( �������̶�� )
        Item_Base item = CreateObj.GetComponent<Item_Base>();
        // �ش�Ǵ� �ν��Ͻ����� ������Ʈ �ҷ����� ( ���Ͷ�� )

        if ( item != null) // ������ ����
        {
            item.ItemSet(createMap);   // �ڵ忡��, ��ġ���� �޼��� ����
        }
        else
        {
            Mob_Base mob_Base = CreateObj.GetComponent<Mob_Base>();
            if ( mob_Base != null )
            {
                mob_Base.StartSet(createMap);
            }
        }
        
    }
}
