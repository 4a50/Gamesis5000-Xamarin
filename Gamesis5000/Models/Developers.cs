using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gamesis5000.Models
{
  public class Developers
  {
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int DevId { get; set; }
    public string Name { get; set; }
  }
}
