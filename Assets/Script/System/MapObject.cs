using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapObject : MonoBehaviour
{
    public int x, z;

    // ������Ʈ�� ��ġ�� �� ȣ��Ǵ� �޼���
    public void SetCellInfo(int a, int b)
    {
        x = a;
        z = b;
    }

}
