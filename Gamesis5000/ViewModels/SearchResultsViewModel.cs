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
      UpdateSearchResults();

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
        foreach (SearchGame sr in searchGames) {
          if (sr.Developer.Count == 0) { sr.Developer.Add(-1); }
          sr.DetailBlurb = $"Release Date: {sr.ReleaseDate}  System: {sr.GameSystem}  Developer: {sr.Developer[0]}";
          SearchResultsList.Add(sr);

          Debug.WriteLine($"GameNameAdded: {sr.Name}");
          Debug.WriteLine($"GameNameAdded: {sr.Description}");        
        }
        Debug.WriteLine($"Entries added SearchResultsList for display: {SearchResultsList.Count}");
      }
      catch (Exception e)
      {
        Debug.WriteLine("[Dev Error] Unable to update SearchResultsList. " + e.Message);
      }
    }
    async public Task<string> PollApi()
    {      
      return await apiServ.QueryDatabase();
    }
    async public Task<int[]> RefreshReference()
    {
      return new int[] {
      await GamesDB.RefreshDeveloper(true), 
      await GamesDB.RefreshGenres(true),
      await GamesDB.RefreshPublishers(true)
    };
    }
  }
}
