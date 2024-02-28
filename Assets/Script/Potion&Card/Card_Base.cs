using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card_Base : MonoBehaviour, IPointerClickHandler
{
    protected Turn_System turn_System;
    protected Potion_System potion_System;
    protected Player player;

    private void Awake()
    {
        turn_System = GameManager.Instance.Turn;
        potion_System = GameManager.Instance.Potion;
        player = GameManager.Instance.Player;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CardEffect();
        Debug.Log("카드 이펙트 & 클릭 성공");
        turn_System.Turn_Reset();
        Destroy(this.gameObject);
    }

    protected virtual void CardEffect()
    {
        // 카드 효과

    }
}
