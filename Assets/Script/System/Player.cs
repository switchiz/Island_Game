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
    /// 플레이어의 좌표 ( x,z )
    /// </summary>
    public int playerX, playerZ;

    /// <summary>
    /// 맵 obj만 나타냄.
    /// </summary>
    int blockMask;

    /// <summary>
    /// 플레이어의 위치
    /// </summary>
    Vector3 Player_pos;

    /// <summary>
    /// 플레이어가 행동했음을 (1턴이 지났음을) 알리는 델리게이트
    /// </summary>
    public Action Turn_Action;

    /// <summary>
    /// 셔플을 하는지 여부
    /// </summary>
    public bool shuffled;

    /// <summary>
    /// 플레이어가 할 행동 ( 0 = 이동 // 1~3 = R G B // 4~6 C P Y // 7~9 B , W , Rainbow , 10 = 행동 정지)
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
        Debug.Log("죽음 ㅇㅇ");
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

        Ray ray = new Ray(transform.position, Vector3.down); // 시작했을때 null을 피하기위해 시작 땅을 기록함
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 1.0f))
        {
            GameObject selectObj = hitInfo.collider.gameObject;
        }
    }

    /// <summary>
    /// 클릭시, 선택한 블럭에 행동을 하는 메서드 
    /// </summary>
    /// <param name="context"></param>
    private void OnClcik(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition); // 마우스 포인터 위치

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
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 10.0f,blockMask)) // 이동
        {
            GameObject selectObj = hitInfo.collider.gameObject;
            MapObject objectkey = selectObj.gameObject.GetComponent<MapObject>();

            if (!(playerX == objectkey.x && playerZ == objectkey.z) && (MathF.Abs(objectkey.x - playerX) <= 1) && (MathF.Abs(objectkey.z - playerZ) <= 1)) // 주위 8칸 선택가능
            {
                if (objectkey.Available_move) // 이동가능 하다면 
                {
                    moveSet(objectkey.x, objectkey.height, objectkey.z); // 이동함
                    playerX = objectkey.x;
                    playerZ = objectkey.z;
                    Turn_Action?.Invoke(); // 1턴 진행
                }
                else if (!objectkey.Available_move && objectkey.available_item) // 이동 불가능하지만, 아이템이 있다면
                {
                    moveSet(objectkey.x, objectkey.height, objectkey.z); // 이동함
                    playerX = objectkey.x;
                    playerZ = objectkey.z;
                    Turn_Action?.Invoke(); // 1턴 진행
                }
            }

        }
    }

    private void castPotion(Ray mouseRay)
    {
        if (Physics.Raycast(mouseRay, out RaycastHit hitInfo, 10.0f, blockMask)) // 선택한게 블럭일때만
        {
            GameObject selectObj = hitInfo.collider.gameObject;
            MapObject objectkey = selectObj.gameObject.GetComponent<MapObject>();
            if ( (MathF.Abs(objectkey.x - playerX) <= 4) && (MathF.Abs(objectkey.z - playerZ) <= 4)) // 주위 4칸 선택가능 , 자기 자신도 선택가능
            {
                Vector3 dir = new Vector3(objectkey.x*0.4f, objectkey.height, objectkey.z*0.4f);
                GameObject potion = Instantiate(potion_Effect[player_Action - 1]);
                potion.transform.position = dir;

                potion_System.potion_EA[player_Action - 1].Potion_number -= 1;
                potion_System.potion_Reset();

                if ( player_Action == 8 )
                {
                    if (objectkey.Available_move) // 이동가능 하다면 
                    {
                        moveSet(objectkey.x, objectkey.height, objectkey.z); // 이동함
                        playerX = objectkey.x;
                        playerZ = objectkey.z;
                    }
                    else if (!objectkey.Available_move && objectkey.available_item) // 이동 불가능하지만, 아이템이 있다면
                    {
                        moveSet(objectkey.x, objectkey.height, objectkey.z); // 이동함
                        playerX = objectkey.x;
                        playerZ = objectkey.z;
                    }
                }



                if ( shuffled )
                PotionShake(0,3);// 하급포션 셔플

                StartCoroutine(Action_Cooltime());
                
            }
            
        }

    }

    IEnumerator Action_Cooltime()
    {
        player_Action = 10;
        yield return new WaitForSeconds(0.5f);
        player_Action = 0;
        Turn_Action?.Invoke(); // 1턴 진행
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
    /// 배열의 특정 부분만 셔플하는 메서드 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array">선택 배열</param>
    /// <param name="startIndex">해당부터 </param>
    /// <param name="length">endIndex + 1</param>
    public static void ShufflePart<T>(T[] array, int startIndex, int length)
    {
        System.Random rng = new System.Random();
        int endIndex = length;
        for (int i = startIndex; i < endIndex; i++)
        {
            int swapIndex = rng.Next(i, endIndex); // i부터 endIndex - 1까지의 랜덤 인덱스
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
            Debug.Log($"회복됨");


        }
    }

}
