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
    /// ������ �ϴ��� ����
    /// </summary>
    public bool shuffled;

    /// <summary>
    /// �÷��̾ �� �ൿ ( 0 = �̵� // 1~3 = R G B // 4~6 C P Y // 7~9 B , W , Rainbow , 10 = �ൿ ����)
    /// </summary>
    public int player_Action = 0;

    /// <summary>
    /// 
    /// </summary>
    public int player_maxhp = 5;

    public int hp;

    public int Hp
    {
        get { return hp; }
        set
        {
            if (hp != value)
            {
                hp = value;
                if (hp <= 0)
                {
                    Dead();
                }
            }
        }
    }

    private void Dead()
    {
        Debug.Log("���� ����");
    }

    private void Awake()
    {
        potion_System = GameManager.Instance.Potion;

        playerinput = new();

        blockMask = 1 << LayerMask.NameToLayer("MapObj");
        
        Hp = player_maxhp;
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
            if ( (MathF.Abs(objectkey.x - playerX) <= 4) && (MathF.Abs(objectkey.z - playerZ) <= 4)) // ���� 4ĭ ���ð��� , �ڱ� �ڽŵ� ���ð���
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



                if ( shuffled )
                PotionShake(0,3);// �ϱ����� ����

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

    public void PotionShake(int a, int b)
    {
        ShufflePart(potion_Effect, a, b); 
    }

    /// <summary>
    /// �迭�� Ư�� �κи� �����ϴ� �޼��� 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array">���� �迭</param>
    /// <param name="startIndex">�ش���� </param>
    /// <param name="length">endIndex + 1</param>
    public static void ShufflePart<T>(T[] array, int startIndex, int length)
    {
        System.Random rng = new System.Random();
        int endIndex = length;
        for (int i = startIndex; i < endIndex; i++)
        {
            int swapIndex = rng.Next(i, endIndex); // i���� endIndex - 1������ ���� �ε���
            T temp = array[i];
            array[i] = array[swapIndex];
            array[swapIndex] = temp;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Potion_Effect"))
        {
            Potion_Effect_Base potion_Effect;
            potion_Effect = other.GetComponent<Potion_Effect_Base>();

            if ( potion_Effect.heal ) Hp++;
            Debug.Log($"ȸ����");


        }
    }

}
