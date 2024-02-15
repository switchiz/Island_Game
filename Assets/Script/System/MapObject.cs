using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapObject : MonoBehaviour
{
    public int x, z;
    public float height;

    /// <summary>
    /// true�� �̵�����, false�� ���úҰ� ( ���� ���� )
    /// </summary>
    public bool available;

    /// <summary>
    /// true�� �̵�����, false�� �̵��Ұ� ( ���¿� ���� ���� )
    /// </summary>
    public bool available_move;

    private void Awake()
    {
        available_move = available;
    }

    // ������Ʈ�� ��ġ�� �� ȣ��Ǵ� �޼���
    public void SetCellInfo(int a, int b)
    {
        x = a;
        z = b;

    }

}
