using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathfind
{
    public AStarGrid grid;
    public AStarPathfind(AStarGrid grid)
    {
        this.grid = grid;
    }

    private float Heuristic(AStarNode a, AStarNode b, bool diagonal=false)
    {
        // 맨해튼 거리
        var dx = Mathf.Abs(a.xPos - b.xPos);
        var dy = Mathf.Abs(a.yPos - b.yPos);

        if(!diagonal) return 1 * (dx + dy);
        // 체비쇼프 거리
        return Mathf.Max(Mathf.Abs(a.xPos - b.xPos), Mathf.Abs(a.yPos - b.yPos));
    }

    public List<AStarNode> CreatePath(AStarNode start, AStarNode end, bool diagonal=false)
    {
        if (start == null || end == null) return null;
        grid.ResetNode();

        List<AStarNode> openSet = new List<AStarNode>();
        List<AStarNode> closedSet = new List<AStarNode>();

        AStarNode startNode = start;
        AStarNode endNode = end;
        startNode.gCost = 0;
        startNode.hCost = Heuristic(start, end);
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            // Open Set 내의 노드 중 가장 거리가 짧은 노드를 찾는다.
            int shortest = 0;
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < openSet[shortest].fCost)
                {
                    shortest = i;
                }
            }
            AStarNode currentNode = openSet[shortest];

            // 목적지 도착
            if (currentNode == endNode)
            {
                Debug.Log("finished");
                // 경로만들어서 반환
                List<AStarNode> path = new List<AStarNode>();
                path.Add(endNode);
                var tempNode = endNode;
                while (tempNode.parent != null)
                {
                    path.Add(tempNode.parent);
                    tempNode = tempNode.parent;
                }
                path.Reverse();
                return path;
            }

            // 리스트를 업데이트한다.
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            // 다음 노드를 방문한다.
            var neighbors = grid.GetNeighborNodes(currentNode, diagonal);
            for (int i = 0; i < neighbors.Count; i++)
            {
                if (closedSet.Contains(neighbors[i]) || !neighbors[i].isWalkable) continue;
                var gCost = currentNode.gCost + Heuristic(currentNode, neighbors[i], diagonal);
                if (gCost < neighbors[i].gCost)
                {
                    neighbors[i].parent = currentNode;
                    neighbors[i].gCost = gCost;
                    neighbors[i].hCost = Heuristic(neighbors[i], endNode, diagonal);
                    if (!openSet.Contains(neighbors[i]))
                        openSet.Add(neighbors[i]);
                }
            }
        }
        return null;
    }

    public List<AStarNode> CreatePath(Vector3Int start, Vector3Int end, bool diagonal)
    {
        AStarNode startNode = grid.GetNodeFromWorld(start);
        AStarNode endNode = grid.GetNodeFromWorld(end);

        var ret = CreatePath(startNode, endNode, diagonal);
        return ret;
    }
}
