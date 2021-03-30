using System;
using System.Collections.Generic;
using System.Text;

namespace Gamesis5000.Models
{
  public class SearchParameters
  {
    public string SearchString { get; set; }
    public string SearchByFilter { get; set; }
    public bool SearchByDatabase { get; set; }
  }
}
