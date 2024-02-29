using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tutorial_Image : MonoBehaviour, IPointerClickHandler
{
    Animator animator;

    readonly int temp = Animator.StringToHash("Tutorial");

    int i = 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (i >= 2)
        {
            Destroy(this.gameObject); 
        }
        

        i++;
        Debug.Log($"{temp},{i}");
        animator.SetInteger(temp, i);
    }
}
