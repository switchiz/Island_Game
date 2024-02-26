using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using Color = UnityEngine.Color;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject[] potion_Effect;

    PlayerInput playerinput;
    Potion_System potion_System;

    /// <summary>
    /// �÷��̾��� ��ǥ ( x,z )
    /// </summary>
    public int playerX, playerZ;

    /// <summary>
    /// �� obj�� ��Ÿ��.
    /// </summary>
    int blockMask;

    /// <summary>
    /// �÷��̾��� ��ġ
    /// </summary>
    Vector3 Player_pos;

    /// <summary>
    /// �÷��̾ �ൿ������ (1���� ��������) �˸��� ��������Ʈ
    /// </summary>
    public Action Turn_Action;

   

    /// <summary>
    /// �÷��̾ �� �ൿ ( 0 = �̵� // 1~3 = R G B // 4~6 C P Y // 7~9 B , W , Rainbow , 10 = �ൿ ����)
    /// </summary>
    public int player_Action = 0;

    private void Awake()
    {
        potion_System = GameManager.Instance.Potion;

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
        }
    }

    /// <summary>
    /// Ŭ����, ������ ���� �ൿ�� �ϴ� �޼��� 
    /// </summary>
    /// <param name="context"></param>
    private void OnClcik(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition); // ���콺 ������ ��ġ

        if (player_Action != 10)
        {
            if (player_Action == 0)
            {
                movePlayer(mouseRay);
            }
            else
            {
                castPotion(mouseRay);
            }
        }


    }



    private void movePlayer(Ray ray) 
    {
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 10.0f,blockMask)) // �̵�
        {
            GameObject selectObj = hitInfo.collider.gameObject;
            MapObject objectkey = selectObj.gameObject.GetComponent<MapObject>();

            if (!(playerX == objectkey.x && playerZ == objectkey.z) && (MathF.Abs(objectkey.x - playerX) <= 1) && (MathF.Abs(objectkey.z - playerZ) <= 1)) // ���� 8ĭ ���ð���
            {
                if (objectkey.Available_move) // �̵����� �ϴٸ� 
                {
                    moveSet(objectkey.x, objectkey.height, objectkey.z); // �̵���
                    playerX = objectkey.x;
                    playerZ = objectkey.z;
                    Turn_Action?.Invoke(); // 1�� ����
                }
                else if (!objectkey.Available_move && objectkey.available_item) // �̵� �Ұ���������, �������� �ִٸ�
                {
                    moveSet(objectkey.x, objectkey.height, objectkey.z); // �̵���
                    playerX = objectkey.x;
                    playerZ = objectkey.z;
                    Turn_Action?.Invoke(); // 1�� ����
                }
            }

        }
    }

    private void castPotion(Ray mouseRay)
    {
        if (Physics.Raycast(mouseRay, out RaycastHit hitInfo, 10.0f, blockMask)) // �����Ѱ� ���϶���
        {
            GameObject selectObj = hitInfo.collider.gameObject;
            MapObject objectkey = selectObj.gameObject.GetComponent<MapObject>();
            if (!(playerX == objectkey.x && playerZ == objectkey.z) && (MathF.Abs(objectkey.x - playerX) <= 4) && (MathF.Abs(objectkey.z - playerZ) <= 4)) // ���� 4ĭ ���ð���
            {
                Vector3 dir = new Vector3(objectkey.x*0.4f, objectkey.height, objectkey.z*0.4f);
                GameObject potion = Instantiate(potion_Effect[player_Action - 1]);
                potion.transform.position = dir;

                potion_System.potion_EA[player_Action - 1].Potion_number -= 1;
                potion_System.potion_Reset();

                if ( player_Action == 8 )
                {
                    if (objectkey.Available_move) // �̵����� �ϴٸ� 
                    {
                        moveSet(objectkey.x, objectkey.height, objectkey.z); // �̵���
                        playerX = objectkey.x;
                        playerZ = objectkey.z;
                    }
                    else if (!objectkey.Available_move && objectkey.available_item) // �̵� �Ұ���������, �������� �ִٸ�
                    {
                        moveSet(objectkey.x, objectkey.height, objectkey.z); // �̵���
                        playerX = objectkey.x;
                        playerZ = objectkey.z;
                    }
                }

                StartCoroutine(Action_Cooltime());
                
            }
            
        }

    }

    IEnumerator Action_Cooltime()
    {
        player_Action = 10;
        yield return new WaitForSeconds(0.5f);
        player_Action = 0;
        Turn_Action?.Invoke(); // 1�� ����
    }

    void moveSet(float x, float y, float z)
    {
        x *= 0.4f;
        z *= 0.4f;
        transform.position = new Vector3(x,y,z);
    }



}
