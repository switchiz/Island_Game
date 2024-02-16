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
    int blockMask;

    Vector3 Player_pos;

    public Action Turn_Action;

    MapObject tempCell; // 밟고있던 땅을 기억하기 위한 임시 저장고

    private void Awake()
    {
        playerinput = new();
        blockMask = 1 << LayerMask.NameToLayer("MapObj");

        
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



        Ray ray = new Ray(transform.position, Vector3.down); // 시작했을때 null을 피하기위해 시작 땅을 기록함
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 1.0f))
        {
            GameObject selectObj = hitInfo.collider.gameObject;
            tempCell = selectObj.gameObject.GetComponent<MapObject>();
            tempCell.available_move = false;
        }
    }

    /// <summary>
    /// 클릭의 위치를 주는 메서드
    /// </summary>
    /// <param name="context"></param>
    private void OnClcik(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition); // 마우스 포인터 위치
        movePlayer(mouseRay); // 위치로 이동
    }


    private void movePlayer(Ray ray) 
    {
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 10.0f,blockMask)) // 이동
        {
            GameObject selectObj = hitInfo.collider.gameObject;
            MapObject objectkey = selectObj.gameObject.GetComponent<MapObject>();


            if (objectkey.available_move && !(tempCell.x == objectkey.x && tempCell.z == objectkey.z) && Mathf.Abs(tempCell.x-objectkey.x) < 2 && Mathf.Abs(tempCell.z - objectkey.z) < 2 )
            {
                moveSet(objectkey.x, objectkey.height, objectkey.z); // 이동함
                objectkey.available_move = false; // 이동한 땅의 move를 false로 만듬
                Debug.Log($"이동{objectkey.x},{objectkey.z}");
                tempCell.available_move = tempCell.available; // 밟고있던 땅 초기화
                tempCell = objectkey; // 이동전 밟고 있던 땅이 기록됨.
                Turn_Action?.Invoke(); // 1턴 진행
            }
        }
    }

    void moveSet(float x, float y, float z)
    {
        x *= 0.4f;
        z *= 0.4f;
        transform.position = new Vector3(x,y,z);

    }



}
