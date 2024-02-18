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


    public Node[,] grid; // 맵오브젝트(셀) 노드를 저장할 2D 배열

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
        depth = lines.Length;
        width = lines[0].Length;

        grid = new Node[width, depth];

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
                        tileset.SetPos(x, depth - 1 - z); // z값을 반전하여 등록

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
        return null; // 잘못 적었으면 null로 내보냄.
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

    public List<Node> FindPath(Vector2Int start, Vector2Int end)
    {
        Debug.Log($"FindPath called with start: {start}, end: {end}");
        Node startNode = grid[start.x, start.y]; //시작 노드 ( 몹 아래 )
        Node endNode = grid[end.x, end.y]; // 도착지 노드 (플레이어)

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode); // 시작 노드를 오픈목록에 넣는다.

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0]; // 시작 노드를 선택한다.

            for (int i = 1; i < openSet.Count; i++) // 오픈목록의 배열만큼 반복한다.
            {
                // F 비용이 낮은 노드, 그중 F 비용이 같다면 H비용이 낮은 노드를 선택
                if (openSet[i].fCost < currentNode.fCost || (openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost))
                {
                    currentNode = openSet[i];
                    Debug.Log($"FindPath processing{currentNode}");
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            /*
            if (currentNode == endNode) // 목적지에 도달했다면 , 경로 재구성 , 도달할 일 없으므로 제외
            {
                return RetracePath(startNode, endNode);
            }
            */

            // 현재 노드의 모든 이웃 노드에 대해 처리
            foreach (Node neighbour in GetNeighbours(currentNode))
            {
                if (!neighbour.available_move || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);

                // 새로운 경로가 더 짧거나 이웃 노드가 열린 집합에 아직 없다면, 이웃 노드를 업데이트
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
                // 현재 노드는 제외
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridZ + y;

                // 그리드 범위를 벗어나지 않는지 확인
                if (checkX >= 0 && checkX < width && checkY >= 0 && checkY < depth)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }





}




