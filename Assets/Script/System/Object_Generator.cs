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

    public GameObject treasure; // 보물상자
    public GameObject rare_treasure; // 상급_보물상자
    public GameObject Lion;     // 사자 ( 몬스터 )
    public GameObject chick;     // 사자 ( 몬스터 )

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
            if (i < 2) CreateObject(rare_treasure); // 1 일때만 
            else CreateObject(treasure);              // 3~10 

            CreateObject(Lion);
            CreateObject(Lion);
        }

        if ( Turn_Count%20 == 0) // 20턴 주기로 박스 생성 10% 확률로 레어 보물상자
        {
            int i = Random.Range(1, 11); // 0~10

            if ( i < 2 ) CreateObject(rare_treasure); // 1 일때만 
            else CreateObject(treasure);              // 3~10 
        }

        if ( Turn_Count%32 == 0) // 32턴 주기로 사자
        {
            CreateObject(Lion);
        }

        if (Turn_Count % 15 == 0) // 15턴 주기로 닭
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

        for (int i = 0; i < checkMap.Length; i++) //  모든 MapObject 확인
        {
            if (checkMap[i].Available_move == true) // i 번째 MapObject가 이동가능이라면,
            {
                moveBlock[temp] = i; // i 번째 블럭을 기록
                temp++;
            }
        }
        createMap = checkMap[moveBlock[Random.Range(0, temp)]];     // 생성할 블럭을 할당

        Vector3 objPos = new Vector3(createMap.x * 0.4f, createMap.height,createMap.z * 0.4f);
        Quaternion creationRotation = Quaternion.identity;

        GameObject CreateObj = Instantiate(obj,objPos,creationRotation); // 해당되는 인스턴스 생성
        


        // 해당되는 인스턴스에서 컴포넌트 불러오기 ( 아이템이라면 )
        Item_Base item = CreateObj.GetComponent<Item_Base>();
        // 해당되는 인스턴스에서 컴포넌트 불러오기 ( 몬스터라면 )

        if ( item != null) // 없으면 안함
        {
            item.ItemSet(createMap);   // 코드에서, 위치지정 메서드 실행
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
