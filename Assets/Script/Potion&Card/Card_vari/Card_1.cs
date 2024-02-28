using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_1 : Card_Base
{
    // 카드 1 : 최대 턴을 15턴 늘린다.
    protected override void CardEffect()
    {
        base.CardEffect();
        turn_System.temp_max_turn = 15;
    }

}
