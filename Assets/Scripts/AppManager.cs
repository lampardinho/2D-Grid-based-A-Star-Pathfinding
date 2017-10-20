using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class AppManager : MonoBehaviour
{
    public event Action<Path> PathfindingFinished = path => { };
    public event Action<Node> StartNodeSet = node => { };
    public event Action<Node> EndNodeSet = node => { };

    [SerializeField] private PointerEventData.InputButton _setStartNodeInputButton = PointerEventData.InputButton.Left;
    [SerializeField] private PointerEventData.InputButton _toggleObstacleInputButton = PointerEventData.InputButton.Middle;
    [SerializeField] private PointerEventData.InputButton _setEndNodeInputButton = PointerEventData.InputButton.Right;

    private Node _startNode;
    private Node _endNode;
    private AStarPathFinder _pathFinder;

    public Grid Grid { get; private set; }

    private void Awake()
    {
        Grid = new Grid();
        _pathFinder = new AStarPathFinder(Grid);
    }

    public void StartPathfinding()
    {
        if (_startNode == null || _endNode == null)
        {
            return;
        }
        var path = _pathFinder.FindPath(_startNode, _endNode);
        PathfindingFinished(path);
    }

    public void ProcessNodeClick(Node node, PointerEventData.InputButton button)
    {
        if (button == _setStartNodeInputButton)
        {
            SetStartNode(node);
        }
        else if (button == _toggleObstacleInputButton)
        {
            Grid.ToggleObstacle(node);
        }
        else if (button == _setEndNodeInputButton)
        {
            SetEndNode(node);
        }
    }

    private void SetStartNode(Node node)
    {
        if (!node.IsWalkable)
        {
            Grid.ToggleObstacle(node);
        }
        if (node == _endNode)
        {
            _endNode = null;
        }
        _startNode = node;
        StartNodeSet(_startNode);
    }

    private void SetEndNode(Node node)
    {
        if (!node.IsWalkable)
        {
            Grid.ToggleObstacle(node);
        }
        if (node == _startNode)
        {
            _startNode = null;
        }
        _endNode = node;
        EndNodeSet(_endNode);
    }
}