using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gamesis5000.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class GameDetailView : ContentPage
  {
    public readonly GameDetailView _vm;
    public GameDetailView()
    {
      InitializeComponent();
      BindingContext = _vm = new GameDetailView();
      //Create an instance of a page. Then navigates to it.
      Title = "View Inventory";
    }
  }
}