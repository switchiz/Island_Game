using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell_System : MonoBehaviour
{
    public Vector2Int worldPosition;
    public bool move_available;
    public int gCost;
    public int hCost;
    public int fCost { get { return gCost + hCost; } }
    public Node parentNode;



    public Cell_System(bool _move, Vector2Int _worldPosition)
    {
        move_available = _move;
        worldPosition = _worldPosition;
    }

}
