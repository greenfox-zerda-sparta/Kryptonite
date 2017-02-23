using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GameServer {
  public class PathFinder {
    private int width;
    private int height;
    private Node[,] nodes;
    private Node startNode;
    private Node endNode;
    private SearchParameters searchParameters;
    private bool[,] map;
    private byte[,] mazePath;
   
    public byte[,] MazePath
    {
      get
      {
        return mazePath;
      }

      set
      {
        mazePath = value;
      }
    }

     public PathFinder(SearchParameters searchParameters)
    {
      this.searchParameters = searchParameters;
      InitializeNodes(searchParameters.Map);
      this.startNode = this.nodes[searchParameters.StartLocation.X, searchParameters.StartLocation.Y];
      this.startNode.State = NodeState.Open;
      this.endNode = this.nodes[searchParameters.EndLocation.X, searchParameters.EndLocation.Y];
    }

    public PathFinder(byte[,] blockmaze)
    {
      CreateBoolArray(blockmaze);
      FindMazePath();
    }

    private void CreateBoolArray(byte[,] blockmaze)
    {
      mazePath = blockmaze;
      map = new bool[blockmaze.GetLength(1), blockmaze.GetLength(0)];
      for (int i = 0; i < blockmaze.GetLength(1); i++)
      {
        for (int j = 0; j < blockmaze.GetLength(0); j++)
        {
          if (Convert.ToBoolean(blockmaze[i, j]) == true)
          {
            map[i, j] = false;
          }
          if (Convert.ToBoolean(blockmaze[i, j]) == false)
          {
            map[i, j] = true;
          }
        }
      }
    }

    private void FindMazePath()
    {
      var startLocation = new Point(1, 1);
      var endLocation = new Point(19, 19);
      this.searchParameters = new SearchParameters(startLocation, endLocation, map);
      PathFinder pathFinder = new PathFinder(searchParameters);
      List<Point> path = pathFinder.FindPath();

      for (UInt16 y = 0; y < mazePath.GetLength(1); y++)
      {
        for (UInt16 x = 0; x < mazePath.GetLength(0); x++)
        {
          if (path.Where(p => p.X == x && p.Y == y).Any() || this.searchParameters.StartLocation.Equals(new Point(x, y)))
          {
            mazePath[x, y] = 3;
          }
          Console.Write(mazePath[x, y]);
        }
        Console.WriteLine();
      }
    }
   
    public List<Point> FindPath()
    {
      List<Point> path = new List<Point>();
      bool success = Search(startNode);
      if (success)
      {
        // If a path was found, follow the parents from the end node to build a list of locations
        Node node = this.endNode;
        while (node.ParentNode != null)
        {
          path.Add(node.Location);
          node = node.ParentNode;
        }
        // Reverse the list so it's in the correct order when returned
        path.Reverse();
      }
      return path;
    }

    private void InitializeNodes(bool[,] map)
    {
      this.width = map.GetLength(0);
      this.height = map.GetLength(1);
      this.nodes = new Node[this.width, this.height];
      for (int y = 0; y < this.height; y++)
      {
        for (int x = 0; x < this.width; x++)
        {
          this.nodes[x, y] = new Node(x, y, map[x, y], this.searchParameters.EndLocation);
        }
      }
    }

    private bool Search(Node currentNode)
    {
      // Set the current node to Closed since it cannot be traversed more than once
      currentNode.State = NodeState.Closed;
      List<Node> nextNodes = GetAdjacentWalkableNodes(currentNode);

      foreach (var nextNode in nextNodes)
      {
        // Check whether the end node has been reached
        if (nextNode.Location == this.endNode.Location)
        {
          return true;
        }
        else
        {
          // If not, check the next set of nodes
          if (Search(nextNode)) // Note: Recurses back into Search(Node)
            return true;
        }
      }
      // The method returns false if this path leads to be a dead end
      return false;
    }

    private List<Node> GetAdjacentWalkableNodes(Node fromNode)
    {
      List<Node> walkableNodes = new List<Node>();
      IEnumerable<Point> nextLocations = GetAdjacentLocations(fromNode.Location);

      foreach (var location in nextLocations)
      {
        int x = location.X;
        int y = location.Y;

        // Stay within the grid's boundaries
        if (x < 0 || x >= this.width || y < 0 || y >= this.height)
          continue;

        Node node = this.nodes[x, y];
        // Ignore non-walkable nodes
        if (!node.IsWalkable)
          continue;

        // Ignore already-closed nodes
        if (node.State == NodeState.Closed)
          continue;

        // Already-open nodes are only added to the list if their G-value is lower going via this route.
        if (node.State == NodeState.Open)
        {
            node.ParentNode = fromNode;
            walkableNodes.Add(node);
        }
        else
        {
          // If it's untested, set the parent and flag it as 'Open' for consideration
          node.ParentNode = fromNode;
          node.State = NodeState.Open;
          walkableNodes.Add(node);
        }
      }
      return walkableNodes;
    }

    private static IEnumerable<Point> GetAdjacentLocations(Point fromLocation)
    {
      return new Point[]
      {
        new Point(fromLocation.X-1, fromLocation.Y  ),
        new Point(fromLocation.X,   fromLocation.Y+1),
        new Point(fromLocation.X+1, fromLocation.Y  ),
        new Point(fromLocation.X,   fromLocation.Y-1)
      };
    }
  }
}