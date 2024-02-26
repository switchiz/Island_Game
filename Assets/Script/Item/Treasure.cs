using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : Item_Base
{
    protected override void PlayerEffect()
    {
        int rand = Random.Range(0, 5);      // 0~4
        int rand_EA = Random.Range(0, 3);   // 0~2

        switch(rand)
        {
            case 0: potion_System.potion_EA[0].Potion_number += rand_EA+1; break; // 레드 포션 + 1~3
            case 1: potion_System.potion_EA[1].Potion_number += rand_EA+1; break; // 레드 포션 + 1~3
            case 2: potion_System.potion_EA[2].Potion_number += rand_EA+1; break; // 레드 포션 + 1~3
            case 3: potion_System.potion_EA[rand_EA].Potion_number += 5; break; // 무작위 포션 + 5
            case 4: potion_System.potion_EA[Random.Range(3, 6)].Potion_number += rand_EA + 1; break; // 중급 포션 + 1~3
        }
        
    }
}
