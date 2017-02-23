using System;
using System.Drawing;

namespace GameServer {

  public class Node {
    private Node parentNode;
    public Point Location { get; private set; }
    public bool IsWalkable { get; set; }
    public NodeState State { get; set; }

    public Node ParentNode
    {
      get { return this.parentNode; }
      set
      {
        this.parentNode = value;
      }
    }

    public Node(int x, int y, bool isWalkable, Point endLocation)
    {
      this.Location = new Point(x, y);
      this.State = NodeState.Untested;
      this.IsWalkable = isWalkable;
    }
  }
}
