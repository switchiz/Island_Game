using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo : MonoBehaviour
{
    public float speed = 1.0f; // ������Ʈ�� �����̴� �ӵ�
    public float height = 25f; // ������Ʈ�� �����̴� �ִ� ����

    private Vector3 startPos; // �ʱ� ��ġ

    void Start()
    {
        startPos = transform.position; // ���� ��ġ�� ���� ��ġ�� ����
    }

    void Update()
    {
        // sin �Լ��� �̿��Ͽ� ���Ʒ� �������� ����
        float newY = Mathf.Sin(Time.time * speed) * height;
        transform.position = startPos + new Vector3(0, newY, 0);
    }
}
