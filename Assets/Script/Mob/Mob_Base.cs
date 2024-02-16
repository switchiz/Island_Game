using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Mob_Base : MonoBehaviour
{
    int Mob_MaxHp = 1;

    int Mob_hp;

    int Mob_x, Mob_z; // 몬스터의 현재 위치를 기록하기 위한 변수

    /// <summary>
    /// 플레이어 발견 ( true면 발견 , false면 미발견 )
    /// </summary>
    bool player_checked;

    MapObject tempCell; // 밟고있던 땅을 기억하기 위한 임시 저장고

    MapObject[] checkMap; // 맵 오브젝트 읽어들이기
    Player player;

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

            Mob_x = tempCell.x;
            Mob_z = tempCell.z;
        }

        checkMap = GameManager.Instance.MapObject;

        player = GameManager.Instance.Player;

        player.Turn_Action += Mob_Action;
    }

    /// <summary>
    /// 플레이어가 행동할때마다 ( 매 턴마다 ) 행동할 행동
    /// </summary>
    private void Mob_Action()
    {
        //move();
        
        if ( !player_checked ) // 플레이어를 발견하지 못했을때 행동
        {
            randomBlockSelect();
        }

    }

    private void move()
    {
        Ray ray = new Ray (transform.position, Vector3.up); // 임시 아무렇게나 적음

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 10.0f)) // 이동
        {
            GameObject selectObj = hitInfo.collider.gameObject;
            MapObject objectkey = selectObj.gameObject.GetComponent<MapObject>();

        if (objectkey.available_move && !(tempCell.x == objectkey.x && tempCell.z == objectkey.z))
        {
            moveSet(objectkey); // 이동함
            objectkey.available_move = false; // 이동한 땅의 move를 false로 만듬
            Debug.Log($"이동{objectkey.x},{objectkey.z}");
            tempCell.available_move = tempCell.available; // 이전에 밟고있던 땅 초기화
            tempCell = objectkey; // 현재 밟고 있는 땅이 tempCell에 기록됨.
        }
        }
    }

    private void randomBlockSelect() // 주위 8칸중 1칸을 선택, 벽이라면 재시도
    {
        int[] moveBlock = new int[8]; // 8칸 생성
        int ArrayBlock = 0; // 배열에 저장할 수 ++ , 초기화


        for (int x = Mob_x - 1; x <= Mob_x + 1; x++) // 주위 8칸을 고름
        {
            for (int z = Mob_z - 1; z <= Mob_z + 1; z++)
            {
                if (x == Mob_x && z == Mob_z) continue; // 자기 셸 건너뛰기

                for (int i = 0; i < checkMap.Length; i++) //  모든 MapObject 확인
                {
                    if (checkMap[i].x == x && checkMap[i].z == z && checkMap[i].available_move == true) // i 번째 MapObject의 좌표가 같고 이동가능이라면,
                    {
                        moveBlock[ArrayBlock] = i; // i 번째 블럭을 기록
                        ArrayBlock++;

                    }
                }
            }
        }

        if (ArrayBlock > 0) // 할당된 요소가 있을 때만 실행
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
