using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo : MonoBehaviour
{
    public float speed = 1.0f; // 오브젝트가 움직이는 속도
    public float height = 25f; // 오브젝트가 움직이는 최대 높이

    private Vector3 startPos; // 초기 위치

    void Start()
    {
        startPos = transform.position; // 시작 위치를 현재 위치로 설정
    }

    void Update()
    {
        // sin 함수를 이용하여 위아래 움직임을 생성
        float newY = Mathf.Sin(Time.time * speed) * height;
        transform.position = startPos + new Vector3(0, newY, 0);
    }
}
