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
      BindingContext = _vm = new SearchResultsViewModel();
    }
    public SearchResultsPage(SearchParameters searchParams)
    {
      BindingContext = _vm = new SearchResultsViewModel();
      searchCriteria = searchParams;
      BindingContext = searchCriteria;
      Debug.WriteLine($"Constructing 2: {searchCriteria.SearchString}");
      InitializeComponent();
    }
  }
}