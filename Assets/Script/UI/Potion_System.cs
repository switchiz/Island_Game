using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Potion_System : MonoBehaviour
{
    Player player;

    [Flags] // 이 enum은 bit flag로 사용한다고 표시하는 어트리뷰트
    enum PotionType : byte
    {
        None = 0,       // 0000 0000
        Red = 1,        // 0000 0001
        Green = 2,      // 0000 0010
        Blue = 4,       // 0000 0100
        Cyan = Green | Blue ,    // 0000 0110 G + B
        Purple = Red | Blue,     // 0000 0101 R + B
        Yellow = Red | Green,    // 0000 0011 R + G
        Black = 16,     // 0001 0000
        White = 32,     // 0010 0000
        RainBow = 64    // 0100 0000
    }

    /// <summary>
    /// 첫번째 선택을 저장하는 변수
    /// </summary>
    PotionType temp_potion;


    private void Awake()
    {
        player = GameManager.Instance.Player;
        // 켜기 |= 끄기 &=
    }

    void select_potion(PotionType potion)
    {
        if ( potion < PotionType.Black ) // 포션이 상급이 아니라면
        {
            if (temp_potion == 0) // 포션이 없다면
            {
                temp_potion = potion; // temp에 포션을 저장한다.
            }
            else // 있다면
            {
                temp_potion |= potion;  // temp 포션을 더한다.
                GivePotion(temp_potion);
                temp_potion = 0;
            }
        }
    }

    void GivePotion(PotionType type)
    {
        switch(type)
        {
            case PotionType.Cyan: break;
            case PotionType.Purple: break;
            case PotionType.Yellow: break;

            default:
                if (type >= PotionType.Black)        // 제작한 포션이 상급 포션이라면
                {
                    break; 
                }
                else                                // 제작한 포션이 조합법에 없다면
                { 
                    temp_potion = 0; // 조합 초기화
                    break;
                }
        }

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
