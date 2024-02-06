using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerInput playerinput;

    /// <summary>
    /// 시작 위치 ( x,z )
    /// </summary>
    public int x, z;

    Vector3 Player_pos;

    private void Awake()
    {
        playerinput = new();
    }

    private void OnEnable()
    {
        playerinput.Player.Enable();
        playerinput.Player.Click.performed += OnClcik;
    }


    private void OnDisable()
    {
        playerinput.Player.Click.performed -= OnClcik;
        playerinput.Player.Disable();
    }



    private void Start()
    {
        transform.position = new Vector3(x*0.4f, 0.4f, z*0.4f);
        Player_pos = transform.position;
        alignPlayer();
    }

    private void Update()
    {
        
    }


    /// <summary>
    /// 클릭의 위치를 주는 메서드
    /// </summary>
    /// <param name="context"></param>
    private void OnClcik(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            MovePlayerTo(hit.point);
            
        }
    }


    /// <summary>
    /// 클릭한곳으로 이동시키는 메서드
    /// </summary>
    private void MovePlayerTo(Vector3 point)
    {
        Debug.Log(point);
        transform.position = point;
        alignPlayer();
        //Vector3 checki = transform.position;
        //checki.y += 0.5f;
    }

    private void alignPlayer()
    {
        MapObject mapObject;



        Ray ray = new Ray(transform.position, -transform.up);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 2.0f) )
        {
            Debug.Log(hitInfo.collider);
             if (hitInfo.collider.gameObject.CompareTag("Floor_1"))
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
