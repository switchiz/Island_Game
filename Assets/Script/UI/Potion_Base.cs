using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Potion_Base : MonoBehaviour, IPointerClickHandler
{
    Potion_System system;

    

    private void Awake()
    {
        system = GameManager.Instance.Potion;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
