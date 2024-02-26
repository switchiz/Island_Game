using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Potion_Base : MonoBehaviour, IPointerClickHandler
{
    Potion_System system;

    [SerializeField]
    private PotionType potionTypePrivate;

    TextMeshProUGUI textMeshProUGUI;

    /// <summary>
    /// 현재 이 포션의 갯수
    /// </summary>
    public int potion_number = 0;


    /// <summary>
    /// 포션 갯수 변동시 발동하는 프로퍼티
    /// </summary>
    public int Potion_number
    {
        get { return potion_number; }
        set
        {
            if (potion_number != value)
            {
                potion_number = value;
                Debug.Log("포션값 변경");
                textMeshProUGUI.text = $"{potion_number}";
            }
        }
    }

    /// <summary>
    /// 선택
    /// </summary>
    bool selected = false;

    private void Awake()
    {
        system = GameManager.Instance.Potion;
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
        textMeshProUGUI.text = $"{potion_number}";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
        if ( potion_number > 0 ) // 포션이 있다면
        {
            system.select_potion(potionTypePrivate); // 포션 선택 전송

            if (!selected)
            {
                selected = true;
            }
            else
            {
                selected = false;
            }
        }


    }
}
