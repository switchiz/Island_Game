using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Grid_System : MonoBehaviour
{
    int width, depth;
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

    public List<Node> FindPath(Vector2Int start, Vector2Int end)
    {
        Debug.Log($"FindPath called with start: {start}, end: {end}");
        Node startNode = grid[start.x, start.y]; //���� ��� ( �� �Ʒ� )
        Node endNode = grid[end.x, end.y]; // ������ ��� (�÷��̾�)

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode); // ���� ��带 ���¸�Ͽ� �ִ´�.

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0]; // ���� ��带 �����Ѵ�.

            for (int i = 1; i < openSet.Count; i++) // ���¸���� �迭��ŭ �ݺ��Ѵ�.
            {
                // F ����� ���� ���, ���� F ����� ���ٸ� H����� ���� ��带 ����
                if (openSet[i].fCost < currentNode.fCost || (openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost))
                {
                    currentNode = openSet[i];
                    Debug.Log($"FindPath processing{currentNode}");
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            /*
            if (currentNode == endNode) // �������� �����ߴٸ� , ��� �籸�� , ������ �� �����Ƿ� ����
            {
                return RetracePath(startNode, endNode);
            }
            */

            // ���� ����� ��� �̿� ��忡 ���� ó��
            foreach (Node neighbour in GetNeighbours(currentNode))
            {
                if (!neighbour.available_move || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);

                // ���ο� ��ΰ� �� ª�ų� �̿� ��尡 ���� ���տ� ���� ���ٸ�, �̿� ��带 ������Ʈ
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, endNode);
                    neighbour.parentNode = currentNode;
                    Debug.Log($"FindPath processing{neighbour}");

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }

        return null;
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int distX = Mathf.Abs(nodeA.worldPosition.x - nodeB.worldPosition.x);
        int distZ = Mathf.Abs(nodeA.worldPosition.y - nodeB.worldPosition.y);

        if (distX > distZ)
            return 14 * distZ + 10 * (distX - distZ);
        return 14 * distX + 10 * (distZ - distX);
    }

    private List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                // ���� ���� ����
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridZ + y;

                // �׸��� ������ ����� �ʴ��� Ȯ��
                if (checkX >= 0 && checkX < width && checkY >= 0 && checkY < depth)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }





}




