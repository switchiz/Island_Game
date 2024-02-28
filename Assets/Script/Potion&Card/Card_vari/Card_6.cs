using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_6 : Card_Base
{
    // 카드 6 : 무작위 중급 포션 + 3
    protected override void CardEffect()
    {
        base.CardEffect();
        int rand = Random.Range(3, 6);
            potion_System.potion_EA[rand].Potion_number += 3;
    }

}
