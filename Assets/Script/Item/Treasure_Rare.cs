using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure_Rare : Item_Base
{
    protected override void PlayerEffect()
    {
        int rand = Random.Range(0, 5);      // 0~4
        int rand_EA = Random.Range(0, 6);   // 0~5

        switch (rand)
        {
            case 0: potion_System.potion_EA[rand_EA].Potion_number += 5; break;                             // 무작위 하급~중급 포션 + 5
            case 1: potion_System.potion_EA[0].Potion_number += 3;                                          // 기본 포션 + 3
                potion_System.potion_EA[1].Potion_number += 3;
                potion_System.potion_EA[2].Potion_number += 3;
                break;

            case 2: potion_System.potion_EA[Random.Range(3,6)].Potion_number += rand_EA + 1; break;             // 중급 포션 + 1~6
            case 3: potion_System.potion_EA[Random.Range(6,9)].Potion_number += 1; break;                   // 상급 포션 + 1
            case 4: potion_System.potion_EA[Random.Range(6, 9)].Potion_number += 1;                  // 중급 + 1 , 상급 + 1
                potion_System.potion_EA[3].Potion_number += 1;                                          
                potion_System.potion_EA[4].Potion_number += 1;
                potion_System.potion_EA[5].Potion_number += 1;
                break;
        }

    }
}
