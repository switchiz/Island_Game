using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

[Flags] // �� enum�� bit flag�� ����Ѵٰ� ǥ���ϴ� ��Ʈ����Ʈ
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
    /// ù��° ������ �����ϴ� ����
    /// </summary>
    PotionType temp_potion = 0;

    /// <summary>
    /// �÷��̾��� �ൿ�� �ӽ� �����ϴ� ����
    /// </summary>
    int temp_Action;


    private void Awake()
    {
        player = GameManager.Instance.Player;
        select.transform.position = new Vector3(2000,2000,0);
        potions = GetComponentsInChildren<Transform>();
        potion_EA = GetComponentsInChildren<Potion_Base>();
        // �ѱ� |= ���� &=
    }

    public void select_potion(PotionType potion)
    {
        if (temp_potion == 0) // temp ������ ����ٸ�
        {
            temp_potion = potion; // temp ������ �����Ѵ�.
            chara_Select(potion); // �÷��̾�� ������ ������ ����.
        }
        else // temp�� ������ �ִٸ�
        {
            select.transform.position = new Vector3(2000, 2000, 0);
            player.player_Action = 0;

            if (potion >= PotionType.Black || temp_potion >= PotionType.Black) // ����� ���� �Ұ�
            {
                potion = 0;
                temp_potion = 0; // temp�� ����.
            }

            if (potion == temp_potion) // ������ ���� �������
            {
                temp_potion = 0;
            }
            else // ���� ������ �ƴ϶��
            {
                if (temp_potion == PotionType.Red || temp_potion == PotionType.Green || temp_potion == PotionType.Blue ) // temp�� �ϱ��̶��
                {
                    if (potion == PotionType.Red || potion == PotionType.Green || potion == PotionType.Blue) // �ϱ� + �ϱ�
                    {
                        temp_potion |= potion; // ������ ��ģ��.
                        GivePotion(temp_potion);
                        
                        temp_potion = 0;
                    }    
                    else // �ϱ� + �߱�
                    {
                        temp_potion = 0;
                    }

                }
                else         
                {
                    if (potion == PotionType.Red || potion == PotionType.Green || potion == PotionType.Blue) // �߱� + �ϱ�
                    {
                        temp_potion = 0;
                    }
                    else // �߱� + �߱�
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
    /// ���� ���� UI
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
            default: // �߱� = �߱��̶�� ��� ���� ����
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




    // ui ���۹�
    // ������ ������, ���� ������ ������ ���� ���
    // ������ ������, �ٴ��� Ŭ���ϸ� �ش� ��ġ�� ���� ȿ�� �ߵ�
    // ������ ������, �ٸ� ���� ����� ������ ������ �ռ�


    // ���� + �Ķ� = ���� 

    // ���� + ��� = ���

    // �Ķ� + ��� = �ϴ�


    // �߱� ���� 2�� = ������ ��� ����
}
