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
            if (!player_checked) // �÷��̾ �߰����� �������� �ൿ
            {
                FindLine(new Vector2Int(Mob_x, Mob_z) );
            }
            else // �÷��̾� �߽߰� �ൿ
            {
                MoveTo(new Vector2Int(Mob_x, Mob_z), new Vector2Int(player.playerX, player.playerZ));
            }
        }
    }



    /// <summary>
    /// �÷��̾ �����ϴ� �޼���
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    private void FindLine(Vector2Int start)
    {
      

        if ( endline == Vector2Int.zero ) // ���� �Ҵ� �ȵǾ��ٸ�,
        {
            Lineset();
        }

        if ((lineMap.x == Mob_x) && (lineMap.z == Mob_z)) // �Ǵ� �������� �����ߴٸ�
        {
            Lineset();
        }

        List<Node> path = grid_sys.FindPath(start, endline); // ���� ��� ���


        if (path == null)
        {
            //Debug.Log("null");
        }

        for (int i = 0; i < checkMap.Length; i++) //  ��� MapObject Ȯ��
        {
            if (checkMap[i].x == path[0].gridX && checkMap[i].z == path[0].gridZ) // i ��° MapObject�� ��ǥ�� ���� �̵������̶��,
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

        for (int i = 0; i < checkMap.Length; i++) //  ��� MapObject Ȯ��
        {
            if (checkMap[i].Available_move == true) // i ��° MapObject�� �̵������̶��,
            {
                moveBlock[temp] = i; // i ��° ���� ���
                temp++;
            }
        }
        lineMap = checkMap[moveBlock[UnityEngine.Random.Range(0, temp)]];
        endline = new Vector2Int(lineMap.x, lineMap.z);
    }
}
