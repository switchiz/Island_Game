using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapObject : MonoBehaviour
{
    public int x, z;

    // 오브젝트가 배치될 때 호출되는 메서드
    public void SetCellInfo(int a, int b)
    {
        x = a;
        z = b;
    }

}
