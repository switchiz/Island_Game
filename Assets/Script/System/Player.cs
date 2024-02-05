using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// 시작 위치 ( x,z )
    /// </summary>
    public int x, z;

    Vector3 Player_pos;

    private void Start()
    {
        transform.position = new Vector3(x*0.4f, 0.4f, z*0.4f);
        Player_pos = transform.position;
    }

    private void Update()
    {
        alignPlayer();
    }

    private void alignPlayer()
    {
        MapObject mapObject;

        Ray ray = new Ray(transform.position, -transform.up);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 2.0f) )
        {
            Debug.Log(hitInfo.collider);
             if ( hitInfo.collider == GameObject.FindGameObjectWithTag("Floor_1") )
            {
                transform.position = new Vector3(transform.position.x, 0.4f, transform.position.z);
            }
             else
            {
                transform.position = new Vector3(transform.position.x, 0.8f, transform.position.z);
            }

        }
    }

}
