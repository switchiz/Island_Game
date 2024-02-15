using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapObject : MonoBehaviour
{
    public int x, z;
    public float height;

    /// <summary>
    /// true면 이동가능, false면 선택불가 ( 영구 저장 )
    /// </summary>
    public bool available;

    /// <summary>
    /// true면 이동가능, false면 이동불가 ( 상태에 따라 변경 )
    /// </summary>
    public bool available_move;

    private void Awake()
    {
        available_move = available;
    }

    // 오브젝트가 배치될 때 호출되는 메서드
    public void SetCellInfo(int a, int b)
    {
        x = a;
        z = b;

    }

}
