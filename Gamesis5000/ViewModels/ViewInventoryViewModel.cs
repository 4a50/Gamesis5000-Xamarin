using Gamesis5000.Models;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Gamesis5000.ViewModels
{
  public class ViewInventoryViewModel : BaseViewModel
  {
    public ObservableRangeCollection<Game> Games { get; set; }
    public ViewInventoryViewModel()
    {
      Title = "Main";
      Games = new ObservableRangeCollection<Game>(); ;
      Task.Run(async () => await GetGameList());
    }
    public async Task GetGameList()
    {
      var allGames = await GamesDB.GetAllGamesAsync();



      try
      {
        foreach (var data in allGames)
        {
          Debug.WriteLine($"[Dev Note] {data.Name} added to Games Collection");
          Games.Add(data);
        }
        Debug.WriteLine("[Dev Note] RangeItemCount: " + allGames.Count);
      }
      catch (Exception e)
      {
        Debug.WriteLine("[Dev Error] Error in populating page: " + e);

      }
    }
  }
}
