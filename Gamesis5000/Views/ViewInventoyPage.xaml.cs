using Gamesis5000.ViewModels;
using System;
using System.Collections.Generic;
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
      await Navigation.PushAsync(new HomePage());
      
    }
  }
}