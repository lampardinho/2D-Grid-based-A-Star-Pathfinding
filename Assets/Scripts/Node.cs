using System;
using System.Collections.Generic;

public class Node
{
    public event Action<bool> WalkableSet = val => { };

    private bool _isWalkable = true;

    public int X { get; private set; }
    public int Y { get; private set; }

    public bool IsWalkable
    {
        get { return _isWalkable; }
        set
        {
            _isWalkable = value;
            WalkableSet(_isWalkable);
        }
    }

    public Node(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class NodePathfindingData
{
    public int DistanceFromStartNode { get; set; }
    public int DistanceToEndNode { get; set; }
    public NodePathfindingData Parent { get; set; }
    public Node Node { get; private set; }

    public NodePathfindingData(Node node)
    {
        Node = node;
    }
}

public class NodesComparer : IComparer<NodePathfindingData>
{
    public int Compare(NodePathfindingData data1, NodePathfindingData data2)
    {
        int res = (data1.DistanceFromStartNode + data1.DistanceToEndNode).CompareTo(data2.DistanceFromStartNode + data2.DistanceToEndNode);
        if (res != 0) return res;

        res = data1.Node.X.CompareTo(data2.Node.X);
        if (res != 0) return res;

        res = data1.Node.Y.CompareTo(data2.Node.Y);
        return res;
    }
}