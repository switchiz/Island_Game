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
        Debug.Log(Turn_Count);

        if ( Turn_Count == 15)
        {
            CreateBox();
        }

    }

    private void CreateBox()
    {
        int[] moveBlock = new int[checkMap.Length];
        int temp = 0;

        for (int i = 0; i < checkMap.Length; i++) //  ��� MapObject Ȯ��
        {
            if (checkMap[i].Available_move == true) // i ��° MapObject�� ��ǥ�� ���� �̵������̶��,
            {
                moveBlock[temp] = i; // i ��° ���� ���
                temp++;
            }
        }
        moveBlock[ Random.Range(0, temp) ] = 0;
    }
}
