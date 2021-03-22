using Gamesis5000.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Gamesis5000.Views
{
  public partial class ItemDetailPage : ContentPage
  {
    public ItemDetailPage()
    {
      InitializeComponent();
      BindingContext = new ItemDetailViewModel();
    }
  }
}