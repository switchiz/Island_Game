using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_8 : Card_Base
{
    // 카드 8 : 하루 길이 - 20
    protected override void CardEffect()
    {
        base.CardEffect();
        turn_System.temp_max_turn = -20;
    }

}
