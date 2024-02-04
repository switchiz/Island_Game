using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapObject : MonoBehaviour
{
    public Vector3 cellPosition;
    public CellType cellType;

    private void Start()
    {
        if ( cellType == CellType.Wall)
        {
            Vector3 WallPos = transform.position;
            WallPos.y += 0.4f;
            transform.position = WallPos;
        }

    }

    // ������Ʈ�� ��ġ�� �� ȣ��Ǵ� �޼���
    public void SetCellInfo(Vector3 position, CellType type)
    {
        cellPosition = position;
        cellType = type;
    }

}
