using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_4 : Card_Base
{
    // 카드 4 : 생명력 최대 회복
    protected override void CardEffect()
    {
        base.CardEffect();
        player.Hp = player.player_maxhp;
    }

}
