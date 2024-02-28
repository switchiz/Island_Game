using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_2 : Card_Base
{
    // 카드 2 : 하급 포션이 무작위가 된다.
    protected override void CardEffect()
    {
        base.CardEffect();
        turn_System.temp_max_turn += 15;
    }

}
