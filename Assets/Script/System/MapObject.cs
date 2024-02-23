using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapObject : MonoBehaviour
{
    public int x, z;


    public Grid_System grid;

    /// <summary>
    /// 이 높이에 따라 오브젝트의 위치 변경
    /// </summary>
    public float height;

    /// <summary>
    /// true면 이동가능, false면 선택불가 ( 영구 저장 )
    /// </summary>
    public bool available;

    /// <summary>
    /// true면 이동가능, false면 이동불가 ( 상태에 따라 변경 )
    /// </summary>
    public bool available_move;

    /// <summary>
    /// true면 Item이 있음. false면 없음
    /// </summary>
    public bool available_item = false;

    /// <summary>
    /// 프로퍼티
    /// </summary>
    public bool Available_move
    {
        get { return available_move; }
        set
        {
            if(available_move != value)
            {
                available_move = value;
                SetNode();
                Debug.Log("노드값 변경");
            }
        }
    }

    private void Awake()
    {
        grid = GameManager.Instance.Grid;
        available_move = available;
        //Debug.Log($"ok {Available_move},{available_move}");
    }

    // 오브젝트가 배치될 때 호출되는 메서드
    public void SetPos(int a, int b)
    {
        x = a;
        z = b;
    }

    public void SetNode()
    {
        Node node = grid.GetNode(x, z);
        node.SetMoveNode(available_move);

    }
}

