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
  public partial class ViewInventoyPage : ContentPage
  {
    readonly ViewInventoryViewModel _vm;
    public ViewInventoyPage()
    {
      InitializeComponent();
      BindingContext = _vm = new ViewInventoryViewModel();
      Title = "View Inventory";
    }
  }
}