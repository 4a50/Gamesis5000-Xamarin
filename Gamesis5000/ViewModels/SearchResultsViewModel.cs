using Gamesis5000.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gamesis5000.ViewModels
{
  class SearchResultsViewModel : BaseViewModel
  {
    public IObservable<SearchParameters> vmParams;
    SearchParameters searchParams;
    SearchResultsViewModel()
    {
      searchParams = new SearchParameters();
    }
    SearchResultsViewModel(SearchParameters inParams)
    {
      searchParams = inParams;
      
    }
  }
}
