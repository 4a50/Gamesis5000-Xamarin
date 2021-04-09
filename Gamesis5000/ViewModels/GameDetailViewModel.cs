using Gamesis5000.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gamesis5000.ViewModels
{
  public class GameDetailViewModel : BaseViewModel
  {
    Game game { get; set; }
    public GameDetailViewModel()
    {
      game = new Game();
    }
    public GameDetailViewModel(Game paramGame)
    {
      game = paramGame;      
    }

  }
}
