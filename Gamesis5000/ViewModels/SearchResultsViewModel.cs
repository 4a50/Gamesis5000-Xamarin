using Gamesis5000.Models;
using System;
using System.Collections.Generic;
using System.Text;
using MvvmHelpers;
using System.ComponentModel;
using System.Diagnostics;
using Gamesis5000.Services;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Data;
using Newtonsoft.Json.Linq;

namespace Gamesis5000.ViewModels
{
  public class SearchResultsViewModel : BaseViewModel, INotifyPropertyChanged
  {
    public IObservable<SearchParameters> vmParams;
    public ObservableRangeCollection<SearchGame> SearchResultsList { get; set; }
    public List<SearchGame> searchGames;
    SearchParameters searchParams;
    string searchString = "";
    APIService apiServ;

    public string SearchStringDisplay { get { return searchString; } }
    public SearchResultsViewModel()
    {
      searchParams = new SearchParameters();
      SearchResultsList = new ObservableRangeCollection<SearchGame>();
    }
    public SearchResultsViewModel(SearchParameters inParams)
    {
      searchParams = inParams;
      searchString = searchParams.SearchString;
      searchGames = new List<SearchGame>();
      SearchResultsList = new ObservableRangeCollection<SearchGame>();
      apiServ = new APIService();

      FetchSearchResults();
      //UpdateSearchResults();

    }
    async void FetchSearchResults()
    {
      string sampleJson = null;
      //Testing Purposes only     
        sampleJson = await apiServ.SampleFileRead();            
      searchGames = apiServ.JsonToSearchGameList(sampleJson);
    }
    void UpdateSearchResults()
    {
      try
      {
        foreach (SearchGame sr in searchGames) { SearchResultsList.Add(sr); }
      }
      catch (Exception e)
      {
        Debug.WriteLine("[Dev Error] Unable to update SearchResultsList. " + e.Message);
      }
    }
  }
}
