using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Mob_Base
{
    protected override void Damaged(Potion_Effect_Base potion_Effect)
    {
        base.Damaged(potion_Effect);
        if (potion_Effect.heal)
        {
            Mob_Hp -= 5;
        }

    }
}
