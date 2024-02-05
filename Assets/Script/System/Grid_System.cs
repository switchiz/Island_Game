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
    public GameObject wallPrefab;  // 2
    public GameObject waterPrefab; // 3
    public GameObject bridgePrefab; // 4
    public GameObject treePrefab; // 5
    public GameObject healingPrefab; // 6
    public GameObject treasurePrefab; // 7
    public GameObject endPrefab; // 8

    private Cell_System[,] grid; // Cell 객체를 저장할 2D 배열

    private void Start()
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
                        prefabToInstantiate = waterPrefab; // 물
                        break;
                    case '3':
                        prefabToInstantiate = wallPrefab; // 벽
                        break;
                    case '4':
                        prefabToInstantiate = treePrefab; // 나무
                        break;
                    case '5':
                        prefabToInstantiate = bridgePrefab; // 다리
                        break;

                }

                if (prefabToInstantiate != null)
                {
                    GameObject tileObj = Instantiate(prefabToInstantiate, position, Quaternion.identity, mapParent.transform);
                }

            }
        }
    }


    /*
    void CreateGrid()
    {
        grid = new Cell_System[width, depth]; // 그리드 = Cell_System[너비,높이]
         GameObject mapParent = new GameObject("Map"); // 오브젝트를 관리할 부모 오브젝트

        for (int x = 0; x < width; x++) // 너비만큼 생성
        {
            for (int z = 0; z < depth; z++) // 높이만큼 생성
            {
                CellType type = CellType.Floor;
                grid[x, z] = new Cell_System(x, z); // 각 셀에 대한 Cell 인스턴스 생성 ( 0,0 ) , (0,1 ) 등
                Vector3 position = GetWorldPosition(x, z); // 오브젝트 좌표 설정

                GameObject obj = Instantiate(objectPrefab, position, Quaternion.identity); // 오브젝트 생성

               
                obj.transform.SetParent(mapParent.transform); // 자식으로 넣기
                obj.GetComponent<MapObject>().SetCellInfo(position, type); // 오브젝트 관리

            }
        }
    }

    */


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




