using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_5 : Card_Base
{
    // ī�� 5 : ��� �ϱ� ���� + 3 , ���� ���� x
    protected override void CardEffect()
    {
        base.CardEffect();
        potion_System.potion_EA[0].Potion_number += 3;
        potion_System.potion_EA[1].Potion_number += 3;
        potion_System.potion_EA[2].Potion_number += 3;
        turn_System.shuffle = false;
    }

}
