using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gamesis5000.Models
{
  public class GameSystem
  {
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int GsId { get; set; }

    public string Name { get; set; }
    public string Developer { get; set; }
    public string Manufacturer { get; set; }
    public string Media { get; set; }
    public string Cpu { get; set; }
    public string Memory { get; set; }
    public string Graphics { get; set; }
    public string Sound { get; set; }
    public string MaxControllers { get; set; }
    public string Display { get; set; }
    public string Overview { get; set; }
    public string Image { get; set; }
    public string VideoUrl { get; set; }

  }
}
