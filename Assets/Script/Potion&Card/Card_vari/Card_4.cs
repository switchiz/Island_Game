using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_4 : Card_Base
{
    // ī�� 4 : ����� �ִ� ȸ��
    protected override void CardEffect()
    {
        base.CardEffect();
        player.Hp = player.player_maxhp;
    }

}
