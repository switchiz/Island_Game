using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_7 : Card_Base
{
    // ī�� 7 : ������ ������� + 3
    protected override void CardEffect()
    {
        base.CardEffect();
        int rand = Random.Range(6, 9);
        potion_System.potion_EA[rand].Potion_number += 3;
    }

}
