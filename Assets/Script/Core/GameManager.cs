using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    Player player;
    public Player Player
    {
        get
        {
            if(player == null)
                player = FindAnyObjectByType<Player>();
            return player;
        }
    }

    MapObject[] mapObject;
    /// <summary>
    /// ������ �Ʒ� ĭ���� ������� ��ϵȴ�. ( x��ǥ ������ 0����, ���� z�� 0���� 1�� ��� )
    /// </summary>
    public MapObject[] MapObject
    {
        get
        {
            if (mapObject == null)
            mapObject = FindObjectsOfType<MapObject>();
            
            return mapObject;

        }
    }

    /*
    Grid_System grid;

    public Grid_System Grid
    {
        get 
        {
            if (grid == null)
                grid = FindAnyObjectByType<Grid_System>();
            Debug.Log("Grid ok");
            return grid; 
        }
    }
    */

    protected override void OnInitialize()
    {
        player = FindAnyObjectByType<Player>();
        mapObject = FindObjectsOfType<MapObject>();
        //grid = FindAnyObjectByType<Grid_System>();

    }



}
