using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using Random = UnityEngine.Random;

public class Mob_Base : MonoBehaviour
{
    int Mob_MaxHp = 1;

    int Mob_hp;

    /// <summary>
    /// // 몬스터의 현재 위치를 기록하기 위한 변수
    /// </summary>
    public int Mob_x, Mob_z; 

    /// <summary>
    /// 플레이어 발견 ( true면 발견 , false면 미발견 )
    /// </summary>
    bool player_checked = false;

    /// <summary>
    /// 플레이어를 발견하는 범위 
    /// </summary>
    public float player_insight = 3;

    /// <summary>
    ///  밟고있던 땅을 기억하기 위한 임시 저장고
    /// </summary>
    MapObject tempCell;

    /// <summary>
    ///  // 맵 오브젝트 읽어들이기
    /// </summary>
    MapObject[] checkMap;

    Player player;

    Grid_System grid_sys;

    private void Awake()
    {
        Mob_hp = Mob_MaxHp;
        grid_sys = GameManager.Instance.Grid;
        // grid_sys = FindAnyObjectByType<Grid_System>();

    }
    private void Start()
    {
        transform.position = new Vector3(Mob_x * 0.4f, 0.4f, Mob_z * 0.4f);


        Ray ray = new Ray(transform.position, Vector3.down); // 시작했을때 null을 피하기위해 시작 땅을 기록함
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 1.0f))
        {
            GameObject selectObj = hitInfo.collider.gameObject;
            tempCell = selectObj.gameObject.GetComponent<MapObject>();
            tempCell.Available_move = false;

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
        if ( !player_checked ) // 플레이어를 발견하지 못했을때 행동
        {
            randomBlockSelect();
        }
        else // 플레이어 발견시 행동
        {
            MoveTo(new Vector2Int(Mob_x, Mob_z), new Vector2Int(player.playerX, player.playerZ));
        }

    }

    /// <summary>
    /// 플레이어를 발견하지 못했을때, 무작위로 이동하는 메서드
    /// </summary>
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
                    if (checkMap[i].x == x && checkMap[i].z == z && checkMap[i].Available_move ) // i 번째 MapObject의 좌표가 같고 이동가능이라면,
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

            // 플레이어가 주변에 있는지 체크함.
            playerchecked();
            
        }
    }

    private void playerchecked() // 기본 찾기 , 몹에 따라 바뀔 수 있음.
    {
        if (Mathf.Abs(player.playerX - Mob_x) < player_insight && Mathf.Abs(player.playerZ - Mob_z) < player_insight)
        {
            Debug.Log("플레이어 발견");
            player_checked = true;
        }
    }


    /// <summary>
    /// 플레이어를 추적하는 메서드
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    public void MoveTo(Vector2Int start, Vector2Int end)
    {

        List<Node> path = grid_sys.FindPath(start, end); // 최적 경로 계산
        if (path == null)
        {
            //Debug.Log("null");
        }

        for (int i = 0; i < checkMap.Length; i++) //  모든 MapObject 확인
        {
            if (checkMap[i].x == path[0].gridX && checkMap[i].z == path[0].gridZ) // i 번째 MapObject의 좌표가 같고 이동가능이라면,
            {
                moveSet(checkMap[i]);

            }
        }

    }

    /// <summary>
    /// 적을 이동시키는 메서드 / 플레이어가 1칸내라면 공격을 실행한다.
    /// </summary>
    /// <param name="obj"></param>
    void moveSet(MapObject obj) // 몹에서는 이동과 동시에, 이동한 발판을 false로 만듦.
    {
        if ( obj.x == player.playerX && obj.z == player.playerZ) // 다음 이동칸에 플레이어가 있다면 공격한다.
        {
            Attack();

        }
        else
        {
            Vector3 objDir = obj.transform.position;
            Vector3 selfDir = transform.position;
            selfDir.y = 0;
            Vector3 direction = ( objDir - selfDir );
            if ( direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = lookRotation;
            }
            Mob_x = obj.x;
            Mob_z = obj.z;
            transform.position = new Vector3(obj.x * 0.4f, obj.height, obj.z * 0.4f);

            obj.Available_move = false; // 이동한 땅의 move를 false로 만듬
            tempCell.Available_move = tempCell.available; // 이전에 밟고있던 땅 초기화
            tempCell = obj; // 현재 밟고 있는 땅이 tempCell에 기록됨.
        }

    }

    /// <summary>
    /// 플레이어와 1칸 내라면 공격한다.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    protected virtual void Attack()
    {
        Debug.Log("플레이어를 공격하였다.");
    }
}
