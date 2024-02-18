using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapObject : MonoBehaviour
{
    public int x, z;


    public Grid_System grid;

    /// <summary>
    /// �� ���̿� ���� ������Ʈ�� ��ġ ����
    /// </summary>
    public float height;

    /// <summary>
    /// true�� �̵�����, false�� ���úҰ� ( ���� ���� )
    /// </summary>
    public bool available;

    /// <summary>
    /// true�� �̵�����, false�� �̵��Ұ� ( ���¿� ���� ���� )
    /// </summary>
    public bool available_move;

    /// <summary>
    /// ������Ƽ
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
            }
        }
    }

    private void Awake()
    {
        grid = FindAnyObjectByType<Grid_System>(); // ���� ���ӸŴ����� �־����. ������ �� �����ִ��� �ȵ�.
        available_move = available;
        //Debug.Log($"ok {Available_move},{available_move}");
    }

    // ������Ʈ�� ��ġ�� �� ȣ��Ǵ� �޼���
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

