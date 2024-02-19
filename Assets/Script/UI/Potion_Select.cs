using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion_System : MonoBehaviour
{
    Player player;

    [Flags] // �� enum�� bit flag�� ����Ѵٰ� ǥ���ϴ� ��Ʈ����Ʈ
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
    /// ù��° ������ �����ϴ� ����
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
            temp_potion |= potion;  // �ѱ� |= ���� &=
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

    


    // ui ���۹�
    // ������ ������, ���� ������ ������ ���� ���
    // ������ ������, �ٴ��� Ŭ���ϸ� �ش� ��ġ�� ���� ȿ�� �ߵ�
    // ������ ������, �ٸ� ���� ����� ������ ������ �ռ�


    // ���� + �Ķ� = ���� 

    // ���� + ��� = ���

    // �Ķ� + ��� = �ϴ�


    // �߱� ���� 2�� = ������ ��� ����
}
