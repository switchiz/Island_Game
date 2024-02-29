using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialButton : MonoBehaviour, IPointerClickHandler
{
    public GameObject tutoObj;
    public void OnPointerClick(PointerEventData eventData)
    {
        Vector2 Pos = new Vector2(1000, 500); 
        Instantiate(tutoObj,Pos,Quaternion.identity,this.transform);
    }

    
}
