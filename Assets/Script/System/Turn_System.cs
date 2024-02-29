using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

public class Turn_System : MonoBehaviour
{
    Player player;
    Grid_System grid_System;
    MapObject[] checkMap;
    MapObject createMap;
    

    [SerializeField]
    private GameObject[] card;

    public Image image;

    public GameObject treasure; // ��������
    public GameObject rare_treasure; // ���_��������
    public GameObject Lion;     // ���� ( ���� )
    public GameObject chick;     // �� ( ���� )
    public GameObject dog;     // �� ( ���� )
    public GameObject skeleton;     // ���̷��� ( ���� )
    public GameObject blur; // ȭ�鰡����

    /// <summary>
    /// �Ϸ� ���� ( �� )
    /// </summary>
    int max_turn = 100;

    /// <summary>
    /// ī��ȿ���� �Ϸ絿�� �����ϴ� ��
    /// </summary>
    public int temp_max_turn = 0;

    /// <summary>
    /// ī��ȿ���� �Ϸ絿�� ���� ���� ����
    /// </summary>
    public bool shuffle = true;

    /// <summary>
    /// ���̵� ( �� ���� �ֱ� )
    /// </summary>
    int difficult_gen;

    /// <summary>
    /// ���̵� ( ��Ÿ ��� )
    /// </summary>
    int difficult;

    int turn_Count = 0;

    int Turn_Count
    {
        get { return turn_Count; }
        set
        {
            if (turn_Count != value)
            {
                turn_Count = value;
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

        image.fillAmount = 1.0f;

        CreateObject(chick);
        CreateObject(dog);
        CreateObject(dog);
        CreateObject(Lion);

        if (difficult >= 4) CreateObject(skeleton);
    }

    void Object_Count()
    {
        
        Turn_Count++;

        image.fillAmount = 1 - ( (float)turn_Count / (max_turn + temp_max_turn));



        if ( Turn_Count%18 == 0) // 18�� �ֱ�� �ڽ� ���� 10% Ȯ���� ���� ��������
        {
            int i = Random.Range(1, 11); // 0~10

            if ( i < 2 ) CreateObject(rare_treasure); // 1 �϶��� 
            else CreateObject(treasure);              // 3~10 
        }

        if (difficult >= 5 && Turn_Count % 65 == 0) // 5�������� ���� ���̷��� ����
        {
            CreateObject(skeleton);
        }

        if ( difficult >= 2 && Turn_Count % (44 - difficult_gen * 2) == 0) // 3�������� ���� +  50�� �ֱ�� ����
        {
            CreateObject(Lion);
        }

        if ( Turn_Count % ( 30 - difficult_gen) == 0) // 30 - �Ϸ縶�� x2 �� �ֱ�� ��
        {
            CreateObject(dog);
        }

        if (Turn_Count % 15 == 0) // 15�� �ֱ�� ��
        {
            CreateObject(chick);
        }

        if ( Turn_Count == max_turn + temp_max_turn )
        {
            temp_max_turn = 0;

            blur.SetActive(true);

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

        // ���� ���߱�
        player.shuffled = false;

        // ������ ī�� ���� , ī�带 �����ϸ� Turn_Reset
        GameObject tileObj = Instantiate(card[Random.Range(0,card.Length)], this.transform.position, Quaternion.identity, this.transform);
        
    }


    public void Turn_Reset()
    {
        // �� + ���� �ʱ�ȭ
        Mob_Base[] mob_Bases = FindObjectsOfType<Mob_Base>();
        Item_Base[] items = FindObjectsOfType<Item_Base>();

        foreach ( Item_Base item_ in items )
        {
            item_.ItemRemove();
        }

        foreach ( Mob_Base mob_Base in mob_Bases )
        {
            mob_Base.Dead();
        }

        // �� �ʱ�ȭ
        if ( shuffle )
        {
            player.PotionShake(0, 3);// �ϱ����� ����
            player.PotionShake(3, 6);// �߱����� ����
        }
        shuffle = true;

        blur.SetActive(false);
        Turn_Count = 0;
        player.player_Action = 0;

        // ���̵� ���
        difficult_gen++;
        difficult++;

        // ù�� �� ����
        int i = Random.Range(1, 11); // 0~10
        if (i < 2) CreateObject(rare_treasure); // 1 �϶��� 
        else CreateObject(treasure);              // 3~10 

        CreateObject(chick);
        CreateObject(chick);
        CreateObject(chick);
        CreateObject(dog);
        CreateObject(dog);
        CreateObject(dog);
        CreateObject(Lion);
        if (difficult >= 4) CreateObject(skeleton);
        max_turn += 12; // ���� 12�Ͼ� �Ϸ簡 �����

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
