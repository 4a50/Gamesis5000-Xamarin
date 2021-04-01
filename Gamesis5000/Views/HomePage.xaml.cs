using Gamesis5000.Models;
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

    public SearchParameters searchParams;


    //readonly HomePageViewModel _vm;
    public ICommand NavigateCommand { get; private set;  }    
    public HomePage()
    {
      InitializeComponent();
      searchParams = new SearchParameters {SearchByFilter = "Title", SearchByDatabase = true };
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

    

    async void OnSearchButtonPressed(object sender, EventArgs e)
    {
      SearchBar search = (SearchBar)sender;
      Text = search.Text;
      searchParams.SearchString = search.Text;
      Debug.WriteLine($"[Dev Note] Search Button Pressed Sending: string: {searchParams.SearchString} filter: {searchParams.SearchByFilter} searchbyDbase: {searchParams.SearchByDatabase}");
      Page page = new Page();
      page = new SearchResultsPage(searchParams);
      await Navigation.PushAsync(page);
    }

    private void OnCheckChanged(object sender, CheckedChangedEventArgs e)
    {
      RadioButton rb = (RadioButton)sender;
      string value = (string)rb.Value;
      if (value == null) Debug.WriteLine("[Dev Critical] value is null");
      Debug.WriteLine($"[Dev Note] rbVal to String: ");
      if (e.Value && value != null)
      {
        if (rb.GroupName == "SearchFilter")
        {
          searchParams.SearchByFilter = value;
        }
        else if (rb.GroupName == "SourceSearch")
        {
          Debug.WriteLine($"[Dev Note] Source Search Updated: ");
          bool.TryParse(value, out bool byDatabase);
          searchParams.SearchByDatabase = byDatabase;
        }
        else
        {
          Debug.WriteLine($"[Dev Note] Nothing Matches Radio Button Values");
        }

        Debug.WriteLine($"[Dev Note] RadioValue: {rb.Value}");
        Debug.WriteLine($"[Dev Note] GroupName: {rb.GroupName}");
      }

      

      //Debug.WriteLine($"[Dev Note] Radio Button {rb.Value}Checked  Args: {e.Value}");
    }
  }
}