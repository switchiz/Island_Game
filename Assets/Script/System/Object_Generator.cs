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

        if ( Turn_Count%3 == 0) // 10턴 주기로 박스 생성 10% 확률로 레어 보물상자
        {
            int i = Random.Range(1, 11); // 0~10

            if ( i < 2 ) CreateObject(rare_treasure); // 1 일때만 
            else CreateObject(treasure);              // 3~10 
        }

        if ( Turn_Count%15 == 0)
        {
            Turn_Count = 0;
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
        Debug.Log($"할당된 블럭{createMap.x},{createMap.z}");

        GameObject CreateObj = Instantiate(obj); // 해당되는 인스턴스 생성


        // 해당되는 인스턴스에서 컴포넌트 불러오기 ( 아이템이라면 )
        Item_Base item = CreateObj.GetComponent<Item_Base>(); 
        if ( item != null) // 없으면 안함
        {
            item.ItemSet(createMap);   // 코드에서, 위치지정 메서드 실행
        }
        
    }
}
