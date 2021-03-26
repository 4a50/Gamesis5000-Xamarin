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
  public partial class InvValidPage : ContentPage
  {
    public InvValidPage()
    {
      InitializeComponent();
    }

    async void OnButtonClick(object sender, EventArgs e)
    {
      Button button = (Button)sender;
      Page page = new Page();
      Debug.WriteLine($"[Dev Note] {button.StyleId} has been triggered");
      switch (button.StyleId)
      {
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