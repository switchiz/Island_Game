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
    /// �÷��̾�� �ε������� üũ
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
        //���� �����ۿ� ���� ȿ�� ����
    }


    /// <summary>
    /// ������ �� ��ġ�� ���ߴ� �޼���
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public void ItemSet(MapObject obj)
    {

        // ��ġ ����
        transform.position = new Vector3(obj.x *0.4f,obj.height,obj.z*0.4f);

        // �ش� ������ �� ���� ��� ( ���� ������� Item off ����� )
        tempObj = obj;

        // �ش� ������ ���� Item on ��Ŵ
        obj.available_item = true;
        obj.Available_move = false;


    }
}
