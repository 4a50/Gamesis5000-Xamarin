using Gamesis5000.Models;
using Gamesis5000.ViewModels;
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
  public partial class SearchResultsPage : ContentPage 
  {
    SearchParameters searchCriteria;
    readonly SearchResultsViewModel _vm;
    public SearchResultsPage()
    {
      InitializeComponent();
      BindingContext = new SearchResultsViewModel();
    }
    public SearchResultsPage(SearchParameters searchParams)
    {
      searchCriteria = searchParams;
      BindingContext = searchCriteria;
      Debug.WriteLine($"Constructing 2: {searchCriteria.SearchString}");
      Title = $"Search Results: {searchCriteria.SearchString}";
      InitializeComponent();

      BindingContext = _vm = new SearchResultsViewModel(searchParams);
    }

    private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
      var listView = (ListView)sender;
      Debug.WriteLine($"[Dev Note] Item Selected args: {e.SelectedItemIndex}");
      Debug.WriteLine($"Name of SearchResults at same index: {_vm.SearchResultsList[e.SelectedItemIndex].Name}");
    }

    async void OnTestPollClick(object sender, EventArgs e)
    {
      await _vm.PollApi();
    }

    async void OnReturnToMainClick(object sender, EventArgs e)
    {
      await Navigation.PushAsync(new HomePage());
    }

    async void OnDevRefreshPollClick(object sender, EventArgs e)
    {

      int[] numRowsAdded = await _vm.RefreshReference();
      
      string display = $"{numRowsAdded[0]} records added to Developers\n{numRowsAdded[1]} records added to Genres\n{numRowsAdded[2]} records added to Publishers";
      await DisplayAlert("Action Completed", display, "Sweet!");

    }
  }
}