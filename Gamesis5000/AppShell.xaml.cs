using Gamesis5000.ViewModels;
using Gamesis5000.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Gamesis5000
{
  public partial class AppShell : Xamarin.Forms.Shell
  {
    public AppShell()
    {
      InitializeComponent();
      //Use this for the navigation buttons on the bottom of the screen
      //Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
      //Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
    }

    private async void OnMenuItemClicked(object sender, EventArgs e)
    {
      await Shell.Current.GoToAsync("//LoginPage");
    }
  }
}
