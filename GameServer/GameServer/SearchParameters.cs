using System.Drawing;

namespace GameServer {

  public class SearchParameters {
    public Point StartLocation { get; set; }

    public Point EndLocation { get; set; }

    public bool[,] Map { get; set; }

    public SearchParameters(Point startLocation, Point endLocation, bool[,] map)
    {
      this.StartLocation = startLocation;
      this.EndLocation = endLocation;
      this.Map = map;
    }
  }
}