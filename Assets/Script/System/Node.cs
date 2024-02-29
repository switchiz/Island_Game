using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using UnityEngine;

public class Node
{
    public int gCost;
    public int hCost;
    public int fCost { get { return gCost + hCost; } }

    public Node parentNode;

    public int gridX, gridZ;

    //public MapObject parentMapObject;

    /// <summary>
    /// true�� �̵�����, false�� �̵��Ұ� ( ���¿� ���� ���� )
    /// </summary>
    public bool available_move;



    /// <summary>
    /// ���̽�Ÿ �˰���� ��ǥ
    /// </summary>
    public Vector2Int worldPosition;


    public Node(bool _move, Vector2Int _worldPos)
    {
        available_move = _move;
        worldPosition = _worldPos;
        
        gridX = _worldPos.x;
        gridZ = _worldPos.y;
    }

    public void SetMoveNode(bool _move )
    {
        available_move = _move;
        //Debug.Log($"{worldPosition},{available_move}");
    }
}
