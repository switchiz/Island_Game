using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public enum Camera_Option
{
    Chara_view
  , Top_view 
  , All_view
}

public class Camera_view : MonoBehaviour
{
    [SerializeField]
    public Camera_Option Camera_op;

    GameObject player;
    public float a;
    public float b;

    private void Awake()
    {
        Camera_op = Camera_Option.Chara_view;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Player_view();
    }

    void Player_view()
    {

        Vector3 PlayerPos = player.transform.position;
        if ( Camera_op == Camera_Option.Chara_view)
        {
            transform.position = new Vector3(PlayerPos.x, PlayerPos.y + 1, PlayerPos.z - 1);
            transform.rotation = Quaternion.Euler(40, 0, 0);
        }
        else if(Camera_op == Camera_Option.Top_view )
        {
            transform.position = new Vector3(PlayerPos.x, PlayerPos.y + 2, PlayerPos.z-0.3f);
            transform.rotation = Quaternion.Euler(80, 0, 0);
        }
        else
        {
            transform.position = new Vector3(PlayerPos.x, PlayerPos.y + 1.5f, PlayerPos.z - 0.5f);
        }




    }


}
