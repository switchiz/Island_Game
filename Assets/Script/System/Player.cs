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
    /// �÷��̾��� ��ǥ ( x,z )
    /// </summary>
    public int playerX, playerZ;
    int blockMask;

    Vector3 Player_pos;

    public Action Turn_Action;

    MapObject tempCell; // ����ִ� ���� ����ϱ� ���� �ӽ� �����

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



        Ray ray = new Ray(transform.position, Vector3.down); // ���������� null�� ���ϱ����� ���� ���� �����
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 1.0f))
        {
            GameObject selectObj = hitInfo.collider.gameObject;
            tempCell = selectObj.gameObject.GetComponent<MapObject>();
            tempCell.available_move = false;
        }
    }

    /// <summary>
    /// Ŭ���� ��ġ�� �ִ� �޼���
    /// </summary>
    /// <param name="context"></param>
    private void OnClcik(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition); // ���콺 ������ ��ġ
        movePlayer(mouseRay); // ��ġ�� �̵�
    }


    private void movePlayer(Ray ray) 
    {
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 10.0f,blockMask)) // �̵�
        {
            GameObject selectObj = hitInfo.collider.gameObject;
            MapObject objectkey = selectObj.gameObject.GetComponent<MapObject>();


            if (objectkey.available_move && !(tempCell.x == objectkey.x && tempCell.z == objectkey.z) && Mathf.Abs(tempCell.x-objectkey.x) < 2 && Mathf.Abs(tempCell.z - objectkey.z) < 2 )
            {
                moveSet(objectkey.x, objectkey.height, objectkey.z); // �̵���
                objectkey.available_move = false; // �̵��� ���� move�� false�� ����
                Debug.Log($"�̵�{objectkey.x},{objectkey.z}");
                tempCell.available_move = tempCell.available; // ����ִ� �� �ʱ�ȭ
                tempCell = objectkey; // �̵��� ��� �ִ� ���� ��ϵ�.
                Turn_Action?.Invoke(); // 1�� ����
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
