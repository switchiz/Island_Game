using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using Color = UnityEngine.Color;

public class Player : MonoBehaviour
{
    PlayerInput playerinput;

    /// <summary>
    /// ���� ��ġ ( x,z )
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
    }

    private void Update()
    {
        
    }


    /// <summary>
    /// Ŭ���� ��ġ�� �ִ� �޼���
    /// </summary>
    /// <param name="context"></param>
    private void OnClcik(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        alignPlayer(ray);
    }

    private void alignPlayer(Ray ray)
    {
        //MapObject mapObject;

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 10.0f) )
        {
            GameObject selectObj = hitInfo.collider.gameObject;
             if (selectObj.CompareTag("Floor_1"))
            {
                transform.position = new Vector3(selectObj.transform.position.x, 0.4f, selectObj.transform.position.z);
            }
             else if(selectObj.CompareTag("Floor_Wall"))
            {

            }
             else
            {
                transform.position = new Vector3(selectObj.transform.position.x, 0.8f, selectObj.transform.position.z);
            }

        }
    }

    private void OnDrawGizmos()
    {
        Vector3 gizmoCellSize = new Vector3(0.4f, 0.4f, 0.4f);
        Vector3 playerGridPosition = transform.position;

      
        
    }

}
