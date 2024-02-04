using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ÿ��
/// </summary>
public enum CellType 
{
    Empty,
    Floor,
    Wall,
    Water
}

public class Cell_System : MonoBehaviour
{
    public Vector3Int Position { get; private set; } // ��ġ
    public CellType Type { get; set; } // �� Ÿ��
    public GameObject Occupant { get; set; } // ���� ��ġ�� ��ü


    /// <summary>
    /// �� ����
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="type"></param>
    public Cell_System(int x, int z, CellType type = CellType.Empty)
    {
        Position = new Vector3Int(x, z);
        Type = type;
    }

}
