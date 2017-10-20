using UnityEngine;
using UnityEngine.EventSystems;

public class GridView : MonoBehaviour
{
    [SerializeField] private GameObject _nodePrefab;
    [SerializeField] private AppManager _manager;

    private NodeView[,] _nodeViews;
    private Path _path;
    private Node _curStartNode;
    private Node _curEndNode;

    private void Start()
    {
        var grid = _manager.Grid;
        _nodeViews = new NodeView[grid.Width, grid.Height];

        for (int i = 0; i < grid.Width; i++)
        {
            for (int j = 0; j < grid.Height; j++)
            {
                _nodeViews[i, j] = Instantiate(_nodePrefab, new Vector3(i, j, 0), Quaternion.identity, transform).GetComponent<NodeView>();
                _nodeViews[i, j].GetComponent<NodeView>().Init(grid.Nodes[i, j]);
                _nodeViews[i, j].GetComponent<NodeView>().Clicked += OnNodeViewClicked;
            }
        }

        _manager.PathfindingFinished += OnPathfindingFinished;
        _manager.StartNodeSet += OnSetStartNode;
        _manager.EndNodeSet += OnSetEndNode;
    }

    private void OnPathfindingFinished(Path path)
    {
        _path = path;
        if (_path != null)
        {
            DrawPath();
        }
    }

    private void OnNodeViewClicked(Node node, PointerEventData.InputButton button)
    {
        if (_path != null)
        {
            ClearPath();
            _path = null;
        }

        _manager.ProcessNodeClick(node, button);
    }

    private void OnSetEndNode(Node node)
    {
        if (_curEndNode != null)
        {
            _nodeViews[_curEndNode.X, _curEndNode.Y].SetWalkable();
        }
        
        _nodeViews[node.X, node.Y].SetEndPoint();
        _curEndNode = node;
    }

    private void OnSetStartNode(Node node)
    {
        if (_curStartNode != null)
        {
            _nodeViews[_curStartNode.X, _curStartNode.Y].SetWalkable();
        }
        
        _nodeViews[node.X, node.Y].SetStartPoint();
        _curStartNode = node;
    }

    private void DrawPath()
    {
        for (int i = 0; i < _path.Nodes.Count - 1; i++)
        {
            var node = _path.Nodes[i];
            _nodeViews[node.X, node.Y].SetPathPoint();
        }
    }

    private void ClearPath()
    {
        for (int i = 0; i < _path.Nodes.Count - 1; i++)
        {
            var node = _path.Nodes[i];
            _nodeViews[node.X, node.Y].SetWalkable();
        }
    }
}
