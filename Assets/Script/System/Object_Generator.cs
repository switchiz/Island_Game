using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Object_Generator : MonoBehaviour
{
    Player player;
    Grid_System grid_System;
    MapObject[] checkMap;
    MapObject createMap;
    TextMeshProUGUI textMeshProUGUI;

    public GameObject treasure; // ��������
    public GameObject rare_treasure; // ���_��������
    public GameObject Lion;     // ���� ( ���� )
    public GameObject chick;     // ���� ( ���� )

    /// <summary>
    /// �Ϸ� ���� ( �� )
    /// </summary>
    int max_turn = 180;

    /// <summary>
    /// ���̵� ( �� ���� �ֱ� )
    /// </summary>
    int diffcult_gen;

    /// <summary>
    /// ���̵� ( ��Ÿ ��� )
    /// </summary>
    int diffcult;

    int turn_Count = 0;

    int Turn_Count
    {
        get { return turn_Count; }
        set
        {
            if (turn_Count != value)
            {
                turn_Count = value;
                textMeshProUGUI.text = $"Turn : {max_turn - turn_Count}";
            }

        }
    }

    private void Start()
    {
        player = GameManager.Instance.Player;
        grid_System = GameManager.Instance.Grid;
        checkMap = GameManager.Instance.MapObject;

        Debug.Log(checkMap.Length);

        player.Turn_Action += Object_Count;

        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
        textMeshProUGUI.text = $"Turn :  {max_turn - turn_Count}";
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

        if (Turn_Count % (50 - diffcult_gen) == 0) // 50�� �ֱ�� ����
        {
            CreateObject(Lion);
        }

        if ( Turn_Count % ( 36 - diffcult_gen) == 0) // 32�� �ֱ�� ����
        {
            CreateObject(Lion);
        }

        if (Turn_Count % 15 == 0) // 15�� �ֱ�� ��
        {
            CreateObject(chick);
        }

        if ( Turn_Count == max_turn )
        {
            Turn_Card();
        }

    }


    /// <summary>
    /// �Ϸ簡 ���� �� ���� ����Ǵ� �Լ�
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void Turn_Card()
    {
        // �÷��̾� Ŭ�� ���� ( UI�� Ŭ������ )
        player.player_Action = 10;

        // ������ ī�� ���� , ī�带 �����ϸ� Turn_Reset


    }


    private void Turn_Reset()
    {
        // �� + ���� �ʱ�ȭ
        GameObject[] gameObjects = GetComponent<GameObject[]>();

        // �� �ʱ�ȭ
        Turn_Count = 0;

        // ���̵� ���
        diffcult_gen++;
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
        //Debug.Log($"{temp}");
        createMap = checkMap[moveBlock[Random.Range(0, temp)]];     // ������ ���� �Ҵ�

        Vector3 objPos = new Vector3(createMap.x * 0.4f, createMap.height,createMap.z * 0.4f);
        Quaternion creationRotation = Quaternion.identity;

        GameObject CreateObj = Instantiate(obj,objPos,creationRotation); // �ش�Ǵ� �ν��Ͻ� ����
        


        // �ش�Ǵ� �ν��Ͻ����� ������Ʈ �ҷ����� ( �������̶�� )
        Item_Base item = CreateObj.GetComponent<Item_Base>();


        if ( item != null) // ������ ����
        {
            item.ItemSet(createMap);   // �ڵ忡��, ��ġ���� �޼��� ����
        }
        else // �ش�Ǵ� �ν��Ͻ����� ������Ʈ �ҷ����� ( ���Ͷ�� )
        {
            Mob_Base mob_Base = CreateObj.GetComponent<Mob_Base>();
            if ( mob_Base != null )
            {
                mob_Base.StartSet(createMap);
            }
        }
        
    }
}
