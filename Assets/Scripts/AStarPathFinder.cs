using System.Collections.Generic;
using UnityEngine;

public class AStarPathFinder
{
    private int _diagonalMoveDistance = 14;
    private int _moveDistance = 10;
    private Grid _grid;
    private NodePathfindingData[,] _nodes;

    public AStarPathFinder(Grid grid)
    {
        _grid = grid;

        _nodes = new NodePathfindingData[_grid.Width, _grid.Height];

        for (int i = 0; i < _grid.Width; i++)
        {
            for (int j = 0; j < _grid.Height; j++)
            {
                _nodes[i, j] = new NodePathfindingData(_grid.Nodes[i, j]);
            }
        }
    }

    public Path FindPath(Node start, Node end)
    {
        var openedNodesList = new OrderedList<NodePathfindingData>(_grid.Width * _grid.Height, new NodesComparer());
        var closedNodesSet = new HashSet<NodePathfindingData>();

        var startNodeData = _nodes[start.X, start.Y];
        var endNodeData = _nodes[end.X, end.Y];

        openedNodesList.Add(startNodeData);

        while (openedNodesList.Count > 0)
        {
            var currentNode = openedNodesList[0];
            openedNodesList.RemoveAt(0);
            closedNodesSet.Add(currentNode);
            
            if (currentNode.Node == end)
            {
                return new Path(startNodeData, endNodeData);
            }

            var nearbyNodes = _grid.GetNearbyNodes(currentNode.Node);
            for (var i = 0; i < nearbyNodes.Count; i++)
            {
                var nearbyNode = _nodes[nearbyNodes[i].X, nearbyNodes[i].Y];
                if (!nearbyNode.Node.IsWalkable || closedNodesSet.Contains(nearbyNode))
                {
                    continue;
                }

                int cost = currentNode.DistanceFromStartNode + GetDistance(currentNode, nearbyNode);
                if (cost >= nearbyNode.DistanceFromStartNode && openedNodesList.Contains(nearbyNode))
                {
                    continue;
                }

                nearbyNode.DistanceFromStartNode = cost;
                nearbyNode.DistanceToEndNode = GetDistance(nearbyNode, endNodeData);
                nearbyNode.Parent = currentNode;
                if (!openedNodesList.Contains(nearbyNode))
                {
                    openedNodesList.Add(nearbyNode);
                }
            }
        }

        return null;
    }

    private int GetDistance(NodePathfindingData data1, NodePathfindingData data2)
    {
        int deltaX = Mathf.Abs(data1.Node.X - data2.Node.X);
        int deltaY = Mathf.Abs(data1.Node.Y - data2.Node.Y);

        if (deltaX > deltaY)
        {
            return _diagonalMoveDistance * deltaY + _moveDistance * (deltaX - deltaY);
        }
        return _diagonalMoveDistance * deltaX + _moveDistance * (deltaY - deltaX);
    }
}

public class Path
{
    public List<Node> Nodes { get; private set; }

    public Path(NodePathfindingData startNode, NodePathfindingData endNode)
    {
        Nodes = new List<Node>();

        var currentNode = endNode;
        while (currentNode != startNode)
        {
            Nodes.Add(currentNode.Node);
            currentNode = currentNode.Parent;
        }
        Nodes.Reverse();
    }
}