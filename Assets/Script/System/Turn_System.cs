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

    public GameObject treasure; // 보물상자
    public GameObject rare_treasure; // 상급_보물상자
    public GameObject Lion;     // 사자 ( 몬스터 )
    public GameObject chick;     // 닭 ( 몬스터 )
    public GameObject dog;     // 개 ( 몬스터 )
    public GameObject skeleton;     // 스켈레톤 ( 몬스터 )
    public GameObject blur; // 화면가리개

    /// <summary>
    /// 하루 길이 ( 턴 )
    /// </summary>
    int max_turn = 100;

    /// <summary>
    /// 카드효과로 하루동안 증가하는 턴
    /// </summary>
    public int temp_max_turn = 0;

    /// <summary>
    /// 카드효과로 하루동안 포션 셔플 여부
    /// </summary>
    public bool shuffle = true;

    /// <summary>
    /// 난이도 ( 몹 생성 주기 )
    /// </summary>
    int difficult_gen;

    /// <summary>
    /// 난이도 ( 기타 등등 )
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



        if ( Turn_Count%18 == 0) // 18턴 주기로 박스 생성 10% 확률로 레어 보물상자
        {
            int i = Random.Range(1, 11); // 0~10

            if ( i < 2 ) CreateObject(rare_treasure); // 1 일때만 
            else CreateObject(treasure);              // 3~10 
        }

        if (difficult >= 5 && Turn_Count % 65 == 0) // 5스테이지 이후 스켈레톤 등장
        {
            CreateObject(skeleton);
        }

        if ( difficult >= 2 && Turn_Count % (44 - difficult_gen * 2) == 0) // 3스테이지 이후 +  50턴 주기로 사자
        {
            CreateObject(Lion);
        }

        if ( Turn_Count % ( 30 - difficult_gen) == 0) // 30 - 하루마다 x2 턴 주기로 개
        {
            CreateObject(dog);
        }

        if (Turn_Count % 15 == 0) // 15턴 주기로 닭
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
    /// 하루가 끝날 때 마다 실행되는 함수
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void Turn_Card()
    {
        // 플레이어 클릭 방지 ( UI만 클릭가능 )
        player.player_Action = 10;

        // 셔플 멈추기
        player.shuffled = false;

        // 무작위 카드 생성 , 카드를 선택하면 Turn_Reset
        GameObject tileObj = Instantiate(card[Random.Range(0,card.Length)], this.transform.position, Quaternion.identity, this.transform);
        
    }


    public void Turn_Reset()
    {
        // 몹 + 상자 초기화
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

        // 턴 초기화
        if ( shuffle )
        {
            player.PotionShake(0, 3);// 하급포션 셔플
            player.PotionShake(3, 6);// 중급포션 셔플
        }
        shuffle = true;

        blur.SetActive(false);
        Turn_Count = 0;
        player.player_Action = 0;

        // 난이도 상승
        difficult_gen++;
        difficult++;

        // 첫턴 몹 생성
        int i = Random.Range(1, 11); // 0~10
        if (i < 2) CreateObject(rare_treasure); // 1 일때만 
        else CreateObject(treasure);              // 3~10 

        CreateObject(chick);
        CreateObject(chick);
        CreateObject(chick);
        CreateObject(dog);
        CreateObject(dog);
        CreateObject(dog);
        CreateObject(Lion);
        if (difficult >= 4) CreateObject(skeleton);
        max_turn += 12; // 매일 12턴씩 하루가 길어짐

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
        //Debug.Log($"{temp}");
        createMap = checkMap[moveBlock[Random.Range(0, temp)]];     // 생성할 블럭을 할당

        Vector3 objPos = new Vector3(createMap.x * 0.4f, createMap.height,createMap.z * 0.4f);
        Quaternion creationRotation = Quaternion.identity;

        GameObject CreateObj = Instantiate(obj,objPos,creationRotation); // 해당되는 인스턴스 생성
        


        // 해당되는 인스턴스에서 컴포넌트 불러오기 ( 아이템이라면 )
        Item_Base item = CreateObj.GetComponent<Item_Base>();


        if ( item != null) // 없으면 안함
        {
            item.ItemSet(createMap);   // 코드에서, 위치지정 메서드 실행
        }
        else // 해당되는 인스턴스에서 컴포넌트 불러오기 ( 몬스터라면 )
        {
            Mob_Base mob_Base = CreateObj.GetComponent<Mob_Base>();
            if ( mob_Base != null )
            {
                mob_Base.StartSet(createMap);
            }
        }
        
    }
}
