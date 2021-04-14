using Gamesis5000.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gamesis5000.ViewModels
{
  public class GameDetailViewModel : BaseViewModel
  {
    public Game game { get; set; }    
    public SearchGame searchGame { get; set; }
    public GameDetailViewModel()
    {
      searchGame = new SearchGame();
      game = new Game();
    }
    public GameDetailViewModel(SearchGame inSearchGame)
    {
      searchGame = inSearchGame;      
    }

  }
}
