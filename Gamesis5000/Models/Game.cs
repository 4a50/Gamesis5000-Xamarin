using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Gamesis5000.Models
{
  public class Game
  {
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public int GameID { get; set; }
    public string Name { get; set; }
    public string GameSystem { get; set; }
    public string Genre { get; set; }
    public string Description { get; set; }
    public string Publisher { get; set; }
    public string Developer { get; set; }
    public string BoxArtUrl { get; set; }
    public string VideoUrl { get; set; }
    public DateTime ReleaseDate { get; set; }
  }
}
