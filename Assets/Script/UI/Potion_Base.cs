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
    /// ���� �� ������ ����
    /// </summary>
    public int potion_number = 0;


    /// <summary>
    /// ���� ���� ������ �ߵ��ϴ� ������Ƽ
    /// </summary>
    public int Potion_number
    {
        get { return potion_number; }
        set
        {
            if (potion_number != value)
            {
                potion_number = value;
                Debug.Log("���ǰ� ����");
                textMeshProUGUI.text = $"{potion_number}";
            }
        }
    }

    /// <summary>
    /// ����
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
        
        if ( potion_number > 0 ) // ������ �ִٸ�
        {
            system.select_potion(potionTypePrivate); // ���� ���� ����

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
