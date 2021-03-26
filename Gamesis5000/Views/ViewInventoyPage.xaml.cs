using Gamesis5000.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gamesis5000.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class ViewInventoyPage : ContentPage
  {
    readonly ViewInventoryViewModel _vm;
    public ICommand NavigateCommand { get; private set; }
    public ViewInventoyPage()
    {
      InitializeComponent();
      BindingContext = _vm = new ViewInventoryViewModel();
      //Create an instance of a page. Then navigates to it.
      Title = "View Inventory";
    }

    async void OnButtonClick(object sender, EventArgs e)
    {
      Button button = (Button)sender;
      Page page = new Page();
      Debug.WriteLine($"[Dev Note] {button.StyleId} has been triggered");
      switch(button.StyleId){
        case "ReturnToMainButton":
          page = new HomePage();
          break;
        default:
          Debug.WriteLine($"[Dev Note] No StyleId Matches Cases");
          break;
      }
      await Navigation.PushAsync(page);
      
    }
  }
}