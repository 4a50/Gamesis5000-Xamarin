using Gamesis5000.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Gamesis5000.ViewModels
{
  public class ManualInventoryEntryViewModel : BaseViewModel
  {
    public Game game { get; set; }
    public ManualInventoryEntryViewModel()
    {
      Title = "Manual Entry VM";
      game = new Game();

    }
    /// <summary>
    /// Populates the Game Model for entry into Database.
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="value"></param>
    /// <param name="dateTime"></param>
    public void EntryUpdateModel(string identifier, string value, DateTime dateTime)
    {
      switch (identifier)
      {
        case "Name":
          game.Name = value;
          break;
        case "GameSystem":
          game.GameSystem = value;
          break;
        case "Description":
          game.Description = value;
          break;
        case "Developer":
          game.Developer = value;
          break;
        case "Publisher":
          game.Publisher = value;
          break;
        case "BoxArt":
          game.BoxArtUrl = value;
          break;
        case "VideoUrl":
          game.VideoUrl = value;
          break;
        case "ReleaseDate":
          game.ReleaseDate = dateTime;
          break;
        default:
          Debug.WriteLine($"Value: {value} does not fit any properties of Game");
          break;

      }
    }
    public async Task TextEntryIntoDataBase()
    {
      try
      { 
        await GamesDB.AddGameAsync(game);
      }
      catch
      {
        Debug.WriteLine("Unable to Insert Game Into Database.  Is game NULL?");
      }
    }


    
  }
}
