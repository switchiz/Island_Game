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

    private Cell_System[,] grid; // Cell ��ü�� ������ 2D �迭

    private void Start()
    {
        LoadMapFromFile("mapData");
    }

    void LoadMapFromFile(string resourcePath)
    {
        TextAsset mapData = Resources.Load<TextAsset>(resourcePath);
        if (mapData == null)
        {
            Debug.Log("���� x" + resourcePath);
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
                Vector3 position = GetWorldPosition(x, depth - 1 - z); // �����Ϳ� �Է��Ѵ�� ����

                GameObject prefabToInstantiate = null; // �ӽ� null 

                switch (tileType)
                {
                    case '0':
                        prefabToInstantiate = floorPrefab; // �ٴ�
                        break;
                    case '1':
                        prefabToInstantiate = floor2Prefab; // 2��
                        break;
                    case '2':
                        prefabToInstantiate = waterPrefab; // ��
                        break;
                    case '3':
                        prefabToInstantiate = wallPrefab; // ��
                        break;
                    case '4':
                        prefabToInstantiate = treePrefab; // ����
                        break;
                    case '5':
                        prefabToInstantiate = bridgePrefab; // �ٸ�
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
        grid = new Cell_System[width, depth]; // �׸��� = Cell_System[�ʺ�,����]
         GameObject mapParent = new GameObject("Map"); // ������Ʈ�� ������ �θ� ������Ʈ

        for (int x = 0; x < width; x++) // �ʺ�ŭ ����
        {
            for (int z = 0; z < depth; z++) // ���̸�ŭ ����
            {
                CellType type = CellType.Floor;
                grid[x, z] = new Cell_System(x, z); // �� ���� ���� Cell �ν��Ͻ� ���� ( 0,0 ) , (0,1 ) ��
                Vector3 position = GetWorldPosition(x, z); // ������Ʈ ��ǥ ����

                GameObject obj = Instantiate(objectPrefab, position, Quaternion.identity); // ������Ʈ ����

               
                obj.transform.SetParent(mapParent.transform); // �ڽ����� �ֱ�
                obj.GetComponent<MapObject>().SetCellInfo(position, type); // ������Ʈ ����

            }
        }
    }

    */


    /// <summary>
    /// ���� ��ġ �׸���
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector3(0,cellSize*0.5f,0), new Vector3(cellSize, cellSize, cellSize));

    }



    /// <summary>
    /// ������Ʈ ��ǥ�� ( 1�� ������ 0.4(cellSize) �� ���´�. )
    /// </summary>
    private Vector3 GetWorldPosition(int x, int z) 
    {

        return new Vector3(x, 0, z) * cellSize;
    }




}




