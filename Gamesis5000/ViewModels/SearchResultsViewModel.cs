﻿using Gamesis5000.Models;
using System;
using System.Collections.Generic;
using System.Text;
using MvvmHelpers;
using System.ComponentModel;
using System.Diagnostics;
using Gamesis5000.Services;
using System.Threading.Tasks;

namespace Gamesis5000.ViewModels
{
  public class SearchResultsViewModel : BaseViewModel, INotifyPropertyChanged
  {
    public IObservable<SearchParameters> vmParams;
    SearchParameters searchParams;
    string searchString = "";
    APIService apiServ;

    public string SearchStringDisplay { get { return searchString; } }
    public SearchResultsViewModel()
    {
      searchParams = new SearchParameters();
    }
    public SearchResultsViewModel(SearchParameters inParams)
    {
      searchParams = inParams;
      searchString = searchParams.SearchString;
      apiServ = new APIService();
      GetSampleText();
    }
    async void GetSampleText() 
    {
      Debug.WriteLine($"TestFile: {await apiServ.GetSampleJSONEntry()}");
    }
  }
}
