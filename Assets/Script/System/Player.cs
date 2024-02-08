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
    /// 플레이어의 좌표 ( x,z )
    /// </summary>
    public int playerX, playerZ;

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
        transform.position = new Vector3(playerX*0.4f, 0.4f, playerZ*0.4f);
        Player_pos = transform.position;
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
        alignPlayer(ray);
    }

    private void alignPlayer(Ray ray)
    {
        //MapObject mapObject;

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 10.0f) )
        {
            GameObject selectObj = hitInfo.collider.gameObject;
            MapObject objectkey = selectObj.gameObject.GetComponent<MapObject>();
            playerX = objectkey.x;
            playerZ = objectkey.z;

             if (selectObj.CompareTag("Floor_1"))
            {
                MoveSet(0.4f);
            }
             else if(selectObj.CompareTag("Floor_Wall"))
            {

            }
             else
            {
                MoveSet(0.8f);
            }

        }
    }

    void MoveSet(float floor)
    {
        transform.position = new Vector3(playerX *0.4f, floor,playerZ *0.4f);
    }

    private void OnDrawGizmos()
    {
        Vector3 gizmoCellSize = new Vector3(0.4f, 0.4f, 0.4f);
        Vector3 playerGridPosition = transform.position;

      
        
    }

}
