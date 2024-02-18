using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Grid_System : MonoBehaviour
{
    private int width;
    private int depth; 
    public float cellSize;
  
    public GameObject floorPrefab; // 0
    public GameObject floor2Prefab; // 1
    public GameObject bridgePrefab; // 2

    //
    //
    //
    
    public GameObject waterPrefab; // 7
    public GameObject wallPrefab;  // 8
    public GameObject treePrefab; // 9


    public Node[,] grid; // �ʿ�����Ʈ(��) ��带 ������ 2D �迭

    private void Awake()
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
        depth = lines.Length;
        width = lines[0].Length;

        grid = new Node[width, depth];

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
                        prefabToInstantiate = bridgePrefab; // �ٸ�
                        break;
                    case '7':
                        prefabToInstantiate = waterPrefab; // ��
                        break;
                    case '8':
                        prefabToInstantiate = wallPrefab; // ��
                        break;
                    case '9':
                        prefabToInstantiate = treePrefab; // ����
                        break;


                }
                

                if (prefabToInstantiate != null)
                {
                    GameObject tileObj = Instantiate(prefabToInstantiate, position, Quaternion.identity, mapParent.transform);
                    MapObject tileset = tileObj.GetComponent<MapObject>();
                    
                    if (tileObj != null)
                    {
                        tileset.SetPos(x, depth - 1 - z); // z���� �����Ͽ� ���

                        grid[x, depth - 1 - z] = new Node(tileset.available_move, new Vector2Int(x, depth - 1 - z));
                        //Debug.Log($"{grid[x, depth - 1 - z].available_move},{x},{depth - 1 - z}");
                    }
                }

            }
        }
    }

    


    public Node GetNode(int x, int z)
    {
        if (x >= 0 && x < width && z >= 0 && z < depth)
        {
            return grid[x, z];
        }
        Debug.Log("not ok");
        return null; // �߸� �������� null�� ������.
    }

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




