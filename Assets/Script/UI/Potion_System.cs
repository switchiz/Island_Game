using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

[Flags] // 이 enum은 bit flag로 사용한다고 표시하는 어트리뷰트
public enum PotionType : byte
{
    None = 0,       // 0000 0000
    Red = 1,        // 0000 0001
    Green = 2,      // 0000 0010
    Blue = 4,       // 0000 0100
    Cyan = Green | Blue,    // 0000 0110 G + B 6
    Purple = Red | Blue,     // 0000 0101 R + B 5
    Yellow = Red | Green,    // 0000 0011 R + G 3

    Check = Red | Green | Blue, // 0000 0111

    Black = 16,     // 0001 0000
    White = 32,     // 0010 0000
    RainBow = 64    // 0100 0000
}

public class Potion_System : MonoBehaviour
{
    Player player;

    public Transform select;
    Transform[] potions;
    public Potion_Base[] potion_EA;

    /// <summary>
    /// 첫번째 선택을 저장하는 변수
    /// </summary>
    PotionType temp_potion = 0;

    /// <summary>
    /// 플레이어의 행동을 임시 저장하는 변수
    /// </summary>
    int temp_Action;


    private void Awake()
    {
        player = GameManager.Instance.Player;
        select.transform.position = new Vector3(2000,2000,0);
        potions = GetComponentsInChildren<Transform>();
        potion_EA = GetComponentsInChildren<Potion_Base>();
        // 켜기 |= 끄기 &=
    }

    public void select_potion(PotionType potion)
    {
        if (temp_potion == 0) // temp 포션이 비었다면
        {
            temp_potion = potion; // temp 포션을 저장한다.
            chara_Select(potion); // 플레이어에게 선택한 포션을 보냄.
        }
        else // temp에 포션이 있다면
        {
            select.transform.position = new Vector3(2000, 2000, 0);
            player.player_Action = 0;

            if (potion >= PotionType.Black || temp_potion >= PotionType.Black) // 상급은 조합 불가
            {
                potion = 0;
                temp_potion = 0; // temp를 비운다.
            }

            if (potion == temp_potion) // 포션이 같은 종류라면
            {
                temp_potion = 0;
            }
            else // 같은 종류가 아니라면
            {
                if (temp_potion == PotionType.Red || temp_potion == PotionType.Green || temp_potion == PotionType.Blue ) // temp가 하급이라면
                {
                    if (potion == PotionType.Red || potion == PotionType.Green || potion == PotionType.Blue) // 하급 + 하급
                    {
                        temp_potion |= potion; // 포션을 합친다.
                        GivePotion(temp_potion);
                        
                        temp_potion = 0;
                    }    
                    else // 하급 + 중급
                    {
                        temp_potion = 0;
                    }

                }
                else         
                {
                    if (potion == PotionType.Red || potion == PotionType.Green || potion == PotionType.Blue) // 중급 + 하급
                    {
                        temp_potion = 0;
                    }
                    else // 중급 + 중급
                    {
                        if (potion == PotionType.Cyan) potion_EA[3].Potion_number -= 1;
                        if (potion == PotionType.Purple) potion_EA[4].Potion_number -= 1;
                        if (potion == PotionType.Yellow) potion_EA[5].Potion_number -= 1;
                        if (temp_potion == PotionType.Cyan) potion_EA[3].Potion_number -= 1;
                        if (temp_potion == PotionType.Purple) potion_EA[4].Potion_number -= 1;
                        if (temp_potion == PotionType.Yellow) potion_EA[5].Potion_number -= 1;
                        GivePotion(PotionType.Check);
                        temp_potion = 0;
                        
                    }
                }
            }
        }
    }

    private void chara_Select(PotionType type)
    {
        switch (type)
        {
            case PotionType.Red: player.player_Action = 1; break;
            case PotionType.Green: player.player_Action = 2; break;
            case PotionType.Blue: player.player_Action = 3; break;

            case PotionType.Cyan: player.player_Action = 4; break;
            case PotionType.Purple: player.player_Action = 5; break;
            case PotionType.Yellow: player.player_Action = 6; break;

            case PotionType.Black: player.player_Action = 7; break;
            case PotionType.White: player.player_Action = 8; break;
            case PotionType.RainBow: player.player_Action = 9; break;
        }
        temp_Action = player.player_Action;

        potionSelect(player.player_Action);
        
    }

    /// <summary>
    /// 포션 선택 UI
    /// </summary>
    /// <param name="a"></param>
    private void potionSelect(int a)
    {
        select.transform.position = potions[a*2].position;
    }

    void GivePotion(PotionType type)
    {
        Debug.Log(type);
        switch(type)
        {
            case PotionType.Cyan: potion_EA[3].Potion_number += 1; potion_EA[1].Potion_number -= 1; potion_EA[2].Potion_number -= 1; break;
            case PotionType.Purple: potion_EA[4].Potion_number += 1; potion_EA[0].Potion_number -= 1; potion_EA[2].Potion_number -= 1; break;
            case PotionType.Yellow: potion_EA[5].Potion_number += 1; potion_EA[0].Potion_number -= 1; potion_EA[1].Potion_number -= 1; break;
            default: // 중급 = 중급이라면 상급 포션 제작
                {
                    int a = Random.Range(0, 3);

                    switch(a)
                    {
                        case 0: potion_EA[6].Potion_number += 1; break;
                        case 1: potion_EA[7].Potion_number += 1; break;
                        case 2: potion_EA[8].Potion_number += 1; break;
                    }
                    
                    break;
                }
        }

    }

    public void potion_Reset()
    {
        temp_potion = 0;
        temp_Action = 0;
        select.transform.position = new Vector3(2000, 2000, 0);
    }




    // ui 조작법
    // 포션을 누르고, 같은 포션을 누르면 선택 취소
    // 포션을 누르고, 바닥을 클릭하면 해당 위치에 포션 효과 발동
    // 포션을 누르고, 다른 같은 등급의 포션을 누르면 합성


    // 빨강 + 파랑 = 보라 

    // 빨강 + 녹색 = 노랑

    // 파랑 + 녹색 = 하늘


    // 중급 포션 2개 = 무작위 상급 포션
}
