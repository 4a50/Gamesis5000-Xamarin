using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gamesis5000.Models
{
  public class Publishers
  {
    [PrimaryKey, AutoIncrement]
    public int IdentityLookup {get; set;}
    public int PubId { get; set; }
    public string Name { get; set; }

  }
}
