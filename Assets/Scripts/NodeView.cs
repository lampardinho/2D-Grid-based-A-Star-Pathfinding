using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeView : MonoBehaviour, IPointerClickHandler
{
    public event Action<Node, PointerEventData.InputButton> Clicked = (node, button) => { };

    [SerializeField] private Color _walkableColor = Color.white;
    [SerializeField] private Color _obstacleColor = Color.red;
    [SerializeField] private Color _startNodeColor = Color.cyan;
    [SerializeField] private Color _endNodeColor = Color.magenta;
    [SerializeField] private Color _pathNodeColor = Color.yellow;
    
    private Node _node;
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

	public void Init(Node node)
    {
        _node = node;
        _node.WalkableSet += val =>
        {
            if (val)
            {
                SetWalkable();
            }
            else
            {
                SetObstacle();
            }
        };
    }

    public void SetWalkable()
    {
        _renderer.color = _walkableColor;
    }

    public void SetObstacle()
    {
        _renderer.color = _obstacleColor;
    }

    public void SetStartPoint()
    {
        _renderer.color = _startNodeColor;
    }

    public void SetEndPoint()
    {
        _renderer.color = _endNodeColor;
    }

    public void SetPathPoint()
    {
        _renderer.color = _pathNodeColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Clicked(_node, eventData.button);
    }
}
