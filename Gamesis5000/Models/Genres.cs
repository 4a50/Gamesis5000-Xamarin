using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gamesis5000.Models
{
  public class Genres
  {
    [PrimaryKey, AutoIncrement]
    public int IdentityLookup {get; set;}
    public string GenreId { get; set; }
    public string Name { get; set; }

  }
}
