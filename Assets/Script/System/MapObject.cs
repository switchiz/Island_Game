using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapObject : MonoBehaviour
{
    public Vector3 cellPosition;

    // ������Ʈ�� ��ġ�� �� ȣ��Ǵ� �޼���
    public void SetCellInfo(Vector3 position)
    {
        cellPosition = position;
    }

}
