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
    /// 몬스터한테 입히는 피해
    /// </summary>
    public int Damage;

    /// <summary>
    /// 회복 수치는 1 고정
    /// </summary>
    public bool heal;

    /// <summary>
    /// 빙결을 거는가 안거는가
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
