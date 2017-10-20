using System.Collections.Generic;

public class Grid
{
    private int _width = 50;
    private int _height = 50;

    public Node[,] Nodes { get; private set; }

    public int Width
    {
        get { return _width; }
        set { _width = value; }
    }

    public int Height
    {
        get { return _height; }
        set { _height = value; }
    }

    public Grid()
    {
        Nodes = new Node[Width, Height];

        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                Nodes[i, j] = new Node(i, j);
            }
        }
    }

    public void ToggleObstacle(Node node)
    {
        node.IsWalkable = !node.IsWalkable;
    }

    public List<Node> GetNearbyNodes(Node node)
    {
        var nodes = new List<Node>();

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                {
                    continue;
                }

                int x = node.X + i;
                int y = node.Y + j;

                if (x < 0 || x >= Width || y < 0 || y >= Height)
                {
                    continue;
                }
                nodes.Add(Nodes[x, y]);
            }
        }

        return nodes;
    }
}
