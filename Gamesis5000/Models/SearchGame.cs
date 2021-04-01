using System;
using System.Collections.Generic;
using System.Text;

namespace Gamesis5000.Models
{
  public class SearchGame
  {
    public int ID { get; set; }
    public int TitleId { get; set; }
    public string Name { get; set; }
    public int GameSystem { get; set; }
    public List<int> Genre { get; set; }
    public string Description { get; set; }
    public List<int> Publisher { get; set; }
    public List<int> Developer { get; set; }
    public string BoxArtUrlFront { get; set; }
    public string BoxArtUrlBack { get; set; }
    public string BoxArtUrlThumb { get; set; }
    public string VideoUrl { get; set; }
    public string DetailBlurb { get; set; }
    public DateTime ReleaseDate { get; set; }
  }
}
