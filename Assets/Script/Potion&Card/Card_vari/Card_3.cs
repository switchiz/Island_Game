using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_3 : Card_Base
{
    // 카드 3 : 무작위 하급 포션 - 1
    protected override void CardEffect()
    {
        base.CardEffect();

        int rand = Random.Range(0, 3);
        if (potion_System.potion_EA[rand].Potion_number != 0)
            potion_System.potion_EA[rand].Potion_number -= 1;
        if (potion_System.potion_EA[rand].Potion_number != 0)
            potion_System.potion_EA[rand].Potion_number -= 1;
        if (potion_System.potion_EA[rand].Potion_number != 0)
            potion_System.potion_EA[rand].Potion_number -= 1;
    }

}
