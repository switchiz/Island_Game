using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_1 : Card_Base
{
    // ī�� 1 : �ִ� ���� 15�� �ø���.
    protected override void CardEffect()
    {
        base.CardEffect();
        turn_System.temp_max_turn = 15;
    }

}
