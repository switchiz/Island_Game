using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using Random = UnityEngine.Random;

public class Mob_Base : MonoBehaviour
{
    public int Mob_MaxHp;

    int Mob_hp;

    protected int Mob_Hp
    {
        get { return Mob_hp; }
        set
        {
            if (Mob_hp != value)
            {
                Mob_hp = value;
                if (Mob_hp <= 0)
                {
                    Dead();
                }
            }
        }
    }



    /// <summary>
    /// // ������ ���� ��ġ�� ����ϱ� ���� ����
    /// </summary>
    public int Mob_x, Mob_z; 

    /// <summary>
    /// �÷��̾� �߰� ( true�� �߰� , false�� �̹߰� )
    /// </summary>
    protected bool player_checked = false;

    /// <summary>
    /// ���� �ð�
    /// </summary>
    protected int Freeze_Duration;

    /// <summary>
    /// �÷��̾ �߰��ϴ� ���� 
    /// </summary>
    public float player_insight = 3;

    /// <summary>
    ///  ����ִ� ���� ����ϱ� ���� �ӽ� �����
    /// </summary>
    protected MapObject tempCell;

    /// <summary>
    ///  // �� ������Ʈ �о���̱�
    /// </summary>
    protected MapObject[] checkMap;

    protected Player player;

    protected Grid_System grid_sys;

    private void Awake()
    {
        Mob_hp = Mob_MaxHp;
        grid_sys = GameManager.Instance.Grid;
        checkMap = GameManager.Instance.MapObject;
        player = GameManager.Instance.Player;
        player.Turn_Action += Mob_Action;
        // grid_sys = FindAnyObjectByType<Grid_System>();
    }


    /// <summary>
    /// �÷��̾ �ൿ�Ҷ����� ( �� �ϸ��� ) �ൿ�� �ൿ
    /// </summary>
    protected virtual void Mob_Action()
    {
        if ( Freeze_Duration > 0 )
        {
            Freeze_Duration--;
        }
        else
        {
            if (!player_checked) // �÷��̾ �߰����� �������� �ൿ
            {
                randomBlockSelect();
            }
            else // �÷��̾� �߽߰� �ൿ
            {
                MoveTo(new Vector2Int(Mob_x, Mob_z), new Vector2Int(player.playerX, player.playerZ));
            }
        }
    }

    /// <summary>
    /// �÷��̾ �߰����� ��������, �������� �̵��ϴ� �޼���
    /// </summary>
    private void randomBlockSelect() // ���� 8ĭ�� 1ĭ�� ����, ���̶�� ��õ�
    {
        int[] moveBlock = new int[8]; // 8ĭ ����
        int ArrayBlock = 0; // �迭�� ������ �� ++ , �ʱ�ȭ


        for (int x = Mob_x - 1; x <= Mob_x + 1; x++) // ���� 8ĭ�� ��
        {
            for (int z = Mob_z - 1; z <= Mob_z + 1; z++)
            {
                if (x == Mob_x && z == Mob_z) continue; // �ڱ� �� �ǳʶٱ�

                for (int i = 0; i < checkMap.Length; i++) //  ��� MapObject Ȯ��
                {
                    if (checkMap[i].x == x && checkMap[i].z == z && checkMap[i].Available_move ) // i ��° MapObject�� ��ǥ�� ���� �̵������̶��,
                    {
                        moveBlock[ArrayBlock] = i; // i ��° ���� ���
                        ArrayBlock++;

                    }
                }
            }
        }

        if (ArrayBlock > 0) // �Ҵ�� ��Ұ� ���� ���� ����
        {
            moveSet(checkMap[moveBlock[Random.Range(0, ArrayBlock)]]);

            // �÷��̾ �ֺ��� �ִ��� üũ��.
            playerchecked();
            
        }
    }

    protected void playerchecked() // �⺻ ã�� , ���� ���� �ٲ� �� ����.
    {
        if (Mathf.Abs(player.playerX - Mob_x) < player_insight && Mathf.Abs(player.playerZ - Mob_z) < player_insight)
        {
            player_checked = true;
        }
    }


    /// <summary>
    /// �÷��̾ �����ϴ� �޼���
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    public void MoveTo(Vector2Int start, Vector2Int end)
    {

        List<Node> path = grid_sys.FindPath(start, end); // ���� ��� ���
        if (path == null)
        {
            //Debug.Log("null");
        }

        for (int i = 0; i < checkMap.Length; i++) //  ��� MapObject Ȯ��
        {
            if (checkMap[i].x == path[0].gridX && checkMap[i].z == path[0].gridZ) // i ��° MapObject�� ��ǥ�� ���� �̵������̶��,
            {
                moveSet(checkMap[i]);

            }
        }

    }

    /// <summary>
    /// ���� �̵���Ű�� �޼��� / �÷��̾ 1ĭ���� �ִٸ� ������ �����Ѵ�.
    /// </summary>
    /// <param name="obj"></param>
    protected virtual void moveSet(MapObject obj) // �������� �̵��� ���ÿ�, �̵��� ������ false�� ����.
    {
        if ( obj.x == player.playerX && obj.z == player.playerZ) // ���� �̵�ĭ�� �÷��̾ �ִٸ� �����Ѵ�.
        {
            Attack();

        }
        else
        {
            Vector3 objDir = obj.transform.position;
            Vector3 selfDir = transform.position;
            selfDir.y = 0;
            Vector3 direction = ( objDir - selfDir );
            if ( direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = lookRotation;
            }
            Mob_x = obj.x;
            Mob_z = obj.z;
            transform.position = new Vector3(obj.x * 0.4f, obj.height, obj.z * 0.4f);

            obj.Available_move = false; // �̵��� ���� move�� false�� ����
            tempCell.Available_move = tempCell.available; // ������ ����ִ� �� �ʱ�ȭ
            tempCell = obj; // ���� ��� �ִ� ���� tempCell�� ��ϵ�.
        }

    }

    /// <summary>
    /// �÷��̾�� 1ĭ ����� �����Ѵ�.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    protected virtual void Attack()
    {
        player.Hp--;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Potion_Effect"))
        {
            Potion_Effect_Base potion_Effect;
            potion_Effect = other.GetComponent<Potion_Effect_Base>();

            Damaged(potion_Effect);


        }
    }

    protected virtual void Damaged(Potion_Effect_Base potion_Effect)
    {
        Mob_Hp -= potion_Effect.Damage;
        Freeze_Duration += potion_Effect.Freeze;
    }

    public void StartSet(MapObject obj)
    {
        // ��ġ ����
        transform.position = new Vector3(obj.x * 0.4f, obj.height, obj.z * 0.4f);

        tempCell = obj.gameObject.GetComponent<MapObject>();
        tempCell.Available_move = false;
        Mob_x = tempCell.x;
        Mob_z = tempCell.z;
    }

    public void Dead()
    {
        tempCell.Available_move = tempCell.available; // ���� ���� �� �ʱ�ȭ
        player.Turn_Action -= Mob_Action; // ��������Ʈ ����
        Destroy(this.gameObject);
    }
}
