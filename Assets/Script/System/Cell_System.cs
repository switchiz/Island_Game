using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell_System : MonoBehaviour
{
    public Vector3Int Position { get; private set; } // ��ġ
    public GameObject Occupant { get; set; } // ���� ��ġ�� ��ü


    /// <summary>
    /// �� ����
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="type"></param>
    public Cell_System(int x, int z)
    {
        Position = new Vector3Int(x, z);

    }

}
