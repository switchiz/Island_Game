using System;
using System.Collections;
using System.Collections.Generic;
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
        //Black = 16,     // 0001 0000
        //White = 32,     // 0010 0000
        //RainBow = 64    // 0100 0000
    }

    /// <summary>
    /// 첫번째 선택을 저장하는 변수
    /// </summary>
    PointerType temp_potion;


    private void Awake()
    {
        player = GameManager.Instance.Player;
    }

    void select_potion(PointerType potion)
    {
        if (temp_potion == 0)
        {
            temp_potion = potion;
        }
        else
        {
            temp_potion |= potion;  // 켜기 |= 끄기 &=
            GivePotion( (PotionType)temp_potion ); 
        }

    }

    void GivePotion(PotionType type)
    {
        

        switch(type)
        {
            case PotionType.Cyan: break;
            case PotionType.Purple: break;
            case PotionType.Yellow: break;
            case PotionType: break;
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
