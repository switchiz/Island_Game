using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_8 : Card_Base
{
    // ī�� 8 : �Ϸ� ���� - 10
    protected override void CardEffect()
    {
        base.CardEffect();
        turn_System.temp_max_turn = -10;
    }

}
