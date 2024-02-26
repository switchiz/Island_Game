using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class Potion_Effect_Base : MonoBehaviour
{
    // ParticleSystem particle;

    /// <summary>
    /// �������� ������ ����
    /// </summary>
    public int Damage;

    /// <summary>
    /// ȸ�� ��ġ�� 1 ����
    /// </summary>
    public bool heal;

    /// <summary>
    /// ������ �Ŵ°� �ȰŴ°�
    /// </summary>
    public int Freeze;


    void Awake()
    {
        // particle = GetComponentInChildren<ParticleSystem>();
        StartCoroutine(Dead());
    }


    IEnumerator Dead()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }


}
