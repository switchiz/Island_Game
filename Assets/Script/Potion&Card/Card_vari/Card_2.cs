using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_2 : Card_Base
{
    // ī�� 2 : �ϱ� ������ �������� �ȴ�.
    protected override void CardEffect()
    {
        base.CardEffect();
        player.shuffled = true;
    }

}