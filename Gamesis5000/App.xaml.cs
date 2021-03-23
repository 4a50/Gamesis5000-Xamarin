using Gamesis5000.Data;
using Gamesis5000.Services;
using Gamesis5000.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gamesis5000
{
  public partial class App : Application
  {

    public App()
    {
      InitializeComponent();          
      DependencyService.Register<GamesisDB>();
      MainPage = new AppShell();
    }

    protected override void OnStart()
    {
    }

    protected override void OnSleep()
    {
    }

    protected override void OnResume()
    {
    }
  }
}
