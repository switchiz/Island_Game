using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lion : Mob_Base
{
    MapObject lineMap;

    Vector2Int endline = Vector2Int.zero;
    protected override void Mob_Action()
    {
        if (Freeze_Duration > 0)
        {
            Freeze_Duration--;
        }
        else
        {
            if (!player_checked) // 플레이어를 발견하지 못했을때 행동
            {
                FindLine(new Vector2Int(Mob_x, Mob_z) );
            }
            else // 플레이어 발견시 행동
            {
                MoveTo(new Vector2Int(Mob_x, Mob_z), new Vector2Int(player.playerX, player.playerZ));
            }
        }
    }



    /// <summary>
    /// 플레이어를 추적하는 메서드
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    private void FindLine(Vector2Int start)
    {
      

        if ( endline == Vector2Int.zero ) // 아직 할당 안되었다면,
        {
            Lineset();
        }

        if ((lineMap.x == Mob_x) && (lineMap.z == Mob_z)) // 또는 목적지에 도착했다면
        {
            Lineset();
        }

        List<Node> path = grid_sys.FindPath(start, endline); // 최적 경로 계산


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
        playerchecked();

    }

    private void Lineset()
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
        lineMap = checkMap[moveBlock[UnityEngine.Random.Range(0, temp)]];
        endline = new Vector2Int(lineMap.x, lineMap.z);
    }
}
