using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_System : MonoBehaviour
{
    private int width;
    private int depth; 
    public float cellSize;
    public Vector3 GridPosition;
    public GameObject floorPrefab;
    public GameObject wallPrefab;
    public GameObject waterPrefab;

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
                        prefabToInstantiate = floorPrefab;
                        break;
                    case '1':
                        prefabToInstantiate = wallPrefab;
                        break;
                    case '2':
                        prefabToInstantiate = waterPrefab;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue; // Gizmo 색상 설정
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            { // Y 대신 Z를 사용
                var cellCenter = GridPosition + new Vector3(x, 0, z) * cellSize;
                Gizmos.DrawWireCube(cellCenter, new Vector3(cellSize, 0, cellSize)); // 셀의 높이를 1칸으로 설정
            }
        }
    }



    /// <summary>
    /// 오브젝트 좌표값 ( 1을 넣으면 0.4(cellSize) 가 나온다. )
    /// </summary>
    private Vector3 GetWorldPosition(int x, int z) 
    {

        return new Vector3(x, 0, z) * cellSize + GridPosition;
    }




}




