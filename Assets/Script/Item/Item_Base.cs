using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Item_Base : MonoBehaviour
{
    MapObject tempObj;
    protected Potion_System potion_System;

    private void Awake()
    {
        potion_System = GameManager.Instance.Potion;
    }

    /// <summary>
    /// 플레이어와 부딪혔는지 체크
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tempObj.Available_move = true;
            tempObj.available_item = false;

            PlayerEffect();

            Destroy(this.gameObject);
        }
    }

    protected virtual void PlayerEffect()
    {
        //먹은 아이템에 따라 효과 발현
    }


    /// <summary>
    /// 생성된 뒤 위치를 맞추는 메서드
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public void ItemSet(MapObject obj)
    {

        // 위치 조정
        transform.position = new Vector3(obj.x *0.4f,obj.height,obj.z*0.4f);

        // 해당 지점의 블럭 값을 기억 ( 추후 사라질때 Item off 만들기 )
        tempObj = obj;

        // 해당 지점의 블럭을 Item on 시킴
        obj.available_item = true;
        obj.Available_move = false;


    }
}
