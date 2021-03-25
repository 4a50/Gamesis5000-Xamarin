using Gamesis5000.ViewModels;
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
  public partial class StartPage : ContentPage
  {
    readonly StartPageViewModel _vm;

    public StartPage()
    {
      InitializeComponent();
      BindingContext = _vm = new StartPageViewModel();
      Title = "Start Page";
    }
  }
}