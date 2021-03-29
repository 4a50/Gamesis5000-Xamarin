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
  public partial class HomePage : ContentPage
  {
  public string Text { get; private set; }

    readonly HomePageViewModel _vm;
    public ICommand NavigateCommand { get; private set;  }    
    public HomePage()
    {
      InitializeComponent();
      //Create an instance of a page. Then navigates to it.
      NavigateCommand = new Command<Type>(async (Type pageType) =>
      {
        Page page = (Page)Activator.CreateInstance(pageType);
        await Navigation.PushAsync(page);
      });
      BindingContext = this;

      Title = "Gamesis 5000X";
    }

    private void OnClick(object sender, EventArgs e)
    {
      Button clickedButton = (Button)sender;
      Debug.WriteLine($"[Dev Note] OnClick() Fired: {clickedButton.StyleId}");
    }

    

    private void OnSearchButtonPressed(object sender, EventArgs e)
    {
      SearchBar search = (SearchBar)sender;
      Text = search.Text;
      Debug.WriteLine($"[Dev Note] Search Button Pressed Text: {search.Text}");
    }
  }
}