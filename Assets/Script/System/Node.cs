using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Node
{
    public int gCost;
    public int hCost;
    public int fCost { get { return gCost + hCost; } }

    public MapObject parentMapObject;

    /// <summary>
    /// true면 이동가능, false면 이동불가 ( 상태에 따라 변경 )
    /// </summary>
    public bool available_move;



    /// <summary>
    /// 에이스타 알고리즘용 좌표
    /// </summary>
    public Vector2Int worldPosition;


    public Node(bool _move, Vector2Int _worldPos)
    {
        available_move = _move;
        worldPosition = _worldPos;
        
    }

    public void SetMoveNode(bool _move )
    {
        available_move = _move;
        Debug.Log($"{worldPosition},{available_move}");
    }
}
