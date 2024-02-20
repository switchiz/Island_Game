using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        Black = 16,     // 0001 0000
        White = 32,     // 0010 0000
        RainBow = 64    // 0100 0000
    }

    /// <summary>
    /// ù��° ������ �����ϴ� ����
    /// </summary>
    PotionType temp_potion;


    private void Awake()
    {
        player = GameManager.Instance.Player;
        // �ѱ� |= ���� &=
    }

    void select_potion(PotionType potion)
    {
        if ( potion < PotionType.Black ) // ������ ����� �ƴ϶��
        {
            if (temp_potion == 0) // ������ ���ٸ�
            {
                temp_potion = potion; // temp�� ������ �����Ѵ�.
            }
            else // �ִٸ�
            {
                temp_potion |= potion;  // temp ������ ���Ѵ�.
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
                if (type >= PotionType.Black)        // ������ ������ ��� �����̶��
                {
                    break; 
                }
                else                                // ������ ������ ���չ��� ���ٸ�
                { 
                    temp_potion = 0; // ���� �ʱ�ȭ
                    break;
                }
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
