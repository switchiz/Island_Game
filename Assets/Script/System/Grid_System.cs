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
        Node startNode = grid[start.x, start.y]; //���� ��� ( �� �Ʒ� )
        Node endNode = grid[end.x, end.y]; // ������ ��� (�÷��̾�)

        List<Node> openSet = new List<Node>(); // ��尡 ���� ���� ����Ʈ
        HashSet<Node> closedSet = new HashSet<Node>(); // ��尡 ���� Ŭ���� ����Ʈ
        openSet.Add(startNode); // ���� ��带 ���¸�Ͽ� �ִ´�.

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0]; // ���� ��带 �����Ѵ�.

            for (int i = 1; i < openSet.Count; i++) // ���� ��带 ������ ���¸���� �迭��ŭ �ݺ��Ѵ�.
            {
                // F ����� ���� ��带 ���� or F ����� ���ٸ� H����� ���� ��带 �����Ѵ�.
                if (openSet[i].fCost < currentNode.fCost || (openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost))
                {
                    currentNode = openSet[i]; // �� ���� ���� ��Ͽ� ����.
                }
            }

            openSet.Remove(currentNode); // ���� ��Ͽ��� ���� ��带 �����Ѵ�.
            closedSet.Add(currentNode); // ���� ���� ���� ��Ͽ� ����.

            
            if (currentNode == endNode) // �������� �����ߴٸ� , ��� �籸��
            {
                return RetracePath(startNode, endNode);
            }
            

            // ���� ����� ��� �̿� ��忡 ���� ó��
            foreach (Node neighbour in GetNeighbours(currentNode))
            {
                if (!neighbour.available_move || closedSet.Contains(neighbour)) // ���̹� ��尡, true�� �ƴϰų� Ŭ����¿� ���� ���
                {
                    continue; // ����
                }

                int newCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour); // ���ó���� gCost�� ( ���ó��� �̿� ���� �Ÿ� ) �� ���Ѵ�.

                // ���ο� ��ΰ� �� ª�ų� �̿� ��尡 ���� ���տ� ���� ���ٸ�, �̿� ��带 ������Ʈ
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeighbour;               // �̿� ����� gCost�� , newCost
                    neighbour.hCost = GetDistance(neighbour, endNode);  // �̿� ����� hCost�� , �̿������� �������� �Ÿ�
                    neighbour.parentNode = currentNode;                 // �̿� ����� parentNode �� ������ ��� (������)

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour); // ���¼¿� ���ٸ� �ִ´�.
                        
                    }
                }
            }

            //Debug.Log(openSet.Count);
        }

        // �������� path�� ���� ���(������ġ)�� ����.
        // ������忡 �������� ���ϴ� ����� null�� ������ ���, ������ �ֵ��� ��.
        List<Node> path = new List<Node>(); // path�� Node�� ��� �迭
        path.Add(startNode);
        return path;
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int distX = Mathf.Abs(nodeA.worldPosition.x - nodeB.worldPosition.x);
        int distZ = Mathf.Abs(nodeA.worldPosition.y - nodeB.worldPosition.y);
        //Debug.Log($"distX = {nodeA.gridX}-{nodeB.gridX} , distZ = {nodeA.gridZ}-{nodeB.gridZ} ");
           

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
                // checkX �� 0�� ���ų� ũ��, checkX�� width���� �۾ƾ���.
                if (checkX >= 0 && checkX < width - 1 && checkY >= 0 && checkY < depth)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    private List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>(); // path�� Node�� ��� �迭
        Node currentNode = endNode; // ���� ��� = ������ ���

        while (currentNode != startNode)  // ���� ��尡 ���� ���� ���� ������ ���� ( ������ �� ���� )
        {
            path.Add(currentNode); // path�� ���� ��带 �ִ´�.
            currentNode = currentNode.parentNode; // ���� ��� = �������� �θ���
        }
        path.Reverse(); // ��θ� ������ �ùٸ� ������ ����ϴ�.
        return path;
    }





}




