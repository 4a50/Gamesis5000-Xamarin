using Gamesis5000.Models;
using Gamesis5000.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gamesis5000.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class GameDetailPage : ContentPage
  {
  readonly GameDetailViewModel _vm;
    public GameDetailPage()
    {
      InitializeComponent();
    }
    public GameDetailPage(SearchGame searchGame)
    {
      InitializeComponent();
      BindingContext = _vm = new GameDetailViewModel(searchGame);
      Debug.WriteLine($"SearchGame Received: {searchGame.Name}");

    }
  }
}