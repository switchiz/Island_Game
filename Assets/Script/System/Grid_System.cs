using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_System : MonoBehaviour
{
    private int width;
    private int depth; 
    public float cellSize;
  
    public GameObject floorPrefab; // 0
    public GameObject floor2Prefab; // 1
    public GameObject bridgePrefab; // 2


    public GameObject waterPrefab; // 7
    public GameObject wallPrefab;  // 8
    public GameObject treePrefab; // 9


    private Cell_System[,] grid; // Cell 객체를 저장할 2D 배열

    private void Awake()
    {
        LoadMapFromFile("mapData");
    }

    void LoadMapFromFile(string resourcePath)
    {
        TextAsset mapData = Resources.Load<TextAsset>(resourcePath);
        if (mapData == null)
        {
            Debug.Log("파일 x" + resourcePath);
            return;
        }

        string[] lines = mapData.text.Split('\n');
        int depth = lines.Length;
        int width = lines[0].Length;

        GameObject mapParent = new GameObject("Map");

        for (int z = 0; z < depth; z++)
        {
            for (int x = 0; x < width-1; x++)
            {
                char tileType = lines[z][x];
                Vector3 position = GetWorldPosition(x, depth - 1 - z); // 데이터에 입력한대로 생성

                GameObject prefabToInstantiate = null; // 임시 null 

                switch (tileType)
                {
                    case '0':
                        prefabToInstantiate = floorPrefab; // 바닥
                        break;
                    case '1':
                        prefabToInstantiate = floor2Prefab; // 2층
                        break;
                    case '2':
                        prefabToInstantiate = bridgePrefab; // 다리
                        break;
                    case '7':
                        prefabToInstantiate = waterPrefab; // 물
                        break;
                    case '8':
                        prefabToInstantiate = wallPrefab; // 벽
                        break;
                    case '9':
                        prefabToInstantiate = treePrefab; // 나무
                        break;


                }
                

                if (prefabToInstantiate != null)
                {
                    GameObject tileObj = Instantiate(prefabToInstantiate, position, Quaternion.identity, mapParent.transform);
                    MapObject tileset = tileObj.GetComponent<MapObject>();
                    
                    if (tileObj != null)
                    {
                        tileset.SetCellInfo(x, depth - 1 - z); // z값을 반전하여 등록
                    }
                }

            }
        }
    }





    /// <summary>
    /// 시작 위치 그리기
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector3(0,cellSize*0.5f,0), new Vector3(cellSize, cellSize, cellSize));

    }



    /// <summary>
    /// 오브젝트 좌표값 ( 1을 넣으면 0.4(cellSize) 가 나온다. )
    /// </summary>
    private Vector3 GetWorldPosition(int x, int z) 
    {

        return new Vector3(x, 0, z) * cellSize;
    }




}




