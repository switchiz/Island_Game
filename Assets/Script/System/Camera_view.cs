using Cinemachine;
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
    public CinemachineVirtualCamera[] vcams;


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

        }
        else if(Camera_op == Camera_Option.Top_view )
        {

        }
        else
        {
            transform.position = new Vector3(PlayerPos.x, PlayerPos.y + 1.5f, PlayerPos.z - 0.5f);
        }




    }


}
