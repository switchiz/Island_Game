using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell_System : MonoBehaviour
{
    public Vector3Int Position { get; private set; } // À§Ä¡
    public GameObject Occupant { get; set; } // ¼¿¿¡ À§Ä¡ÇÑ °´Ã¼


    /// <summary>
    /// ¼¿ »ý¼º
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="type"></param>
    public Cell_System(int x, int z)
    {
        Position = new Vector3Int(x, z);

    }

}
