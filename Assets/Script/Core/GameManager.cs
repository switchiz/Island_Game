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

    Potion_System potion;
    public Potion_System Potion
    {
        get
        {

            if (potion == null)
                Debug.Log("ok");
                potion = FindAnyObjectByType<Potion_System>();
            return potion;
        }
    }

    MapObject[] mapObject;
    /// <summary>
    /// 오른쪽 아래 칸부터 순서대로 등록된다. ( x좌표 끝부터 0까지, 그후 z는 0부터 1씩 상승 )
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


    protected override void OnInitialize()
    {
        player = FindAnyObjectByType<Player>();
        potion = FindAnyObjectByType<Potion_System>();
        mapObject = FindObjectsOfType<MapObject>();
        grid = FindAnyObjectByType<Grid_System>();

    }



}
