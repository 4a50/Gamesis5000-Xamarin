using Gamesis5000.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Gamesis5000.Services
{
  public class APIService
  {
    HttpClient client;
    string longUrl = "https://api.thegamesdb.net/v1.1/Games/ByGameName?apikey=4c9d180c15bf5fe3e896c204472a85c752dddb4fcdf0ba291b00b037af1c1910&name=Super%20Mario%20Bros.%203&fields=platform%2Cyoutube%2Cgenres&include=boxart%2Cplatform";
    //string BaseUrl = "https://api.thegamesdb.net/v1/Games/ByGameName?apikey=4c9d180c15bf5fe3e896c204472a85c752dddb4fcdf0ba291b00b037af1c1910&name=super%20Metroid&filter%5Bplatform%5D=6";
    //string byTitle = 
    string jsonString = "";



    // This will be just a file access for a while until I can get the JSON parsed out correctly.    
    const string localFileName = "MetroidGamesDBResponse.json";    
    //
    public APIService()
    {
      client = new HttpClient();
    }   
    public async Task<string> QueryDatabase()
    {
      string jsonOutputString = "";
      Uri uri = new Uri(longUrl);
      HttpResponseMessage res = await client.GetAsync(uri);
      if (res.IsSuccessStatusCode)
      {
        jsonString = await res.Content.ReadAsStringAsync();
        Debug.WriteLine($"[Dev Output] {jsonString}");
      }
      return jsonOutputString;
    }
    public async Task<string> SampleFileRead()
    {
      //Local File.  Do Not use when accessing actual API
      string textOutput;
      using (var stream = await FileSystem.OpenAppPackageFileAsync(localFileName))
      {
        using (var reader = new StreamReader(stream))
        {
          textOutput = reader.ReadToEnd();
        }        
      }
      //      
      return textOutput;
    }

    public List<SearchGame> JsonToSearchGameList(string jsonString)
    {      
      JObject jsonObj = JObject.Parse(jsonString);
      List<SearchGame> jsonGameList = new List<SearchGame>();
      try
      {
        Debug.WriteLine($"[Dev Note] jsonGameListLength: {jsonObj["data"]["games"].Count()}");
        jsonGameList = jsonObj["data"]["games"]
          .Select(game => new SearchGame
          {
            Name = (string)game["game_title"],
            TitleId = (int)game["id"],
            ReleaseDate = DateTime.Parse((string)game["release_date"]),
            Genre = game["genres"].Select(g => (int)g).ToList(),
            GameSystem = (int)game["platform"],
            Description = (string)game["overview"],
            Developer = game["developers"]            
            .Select(d => (int)d)            
            .ToList(),
            Publisher = (game["publishers"] == null ? new List<int> { -1 } : game["publishers"]            
            .Select(p => (int)p)
            .ToList())
          }).ToList();
        Debug.WriteLine($"[Dev Note] Post-Conversion jsonGameListLength: {jsonGameList.Count}");
      }
      catch(Exception e)
      {
        Debug.WriteLine($"[Dev Error] jsonGameList Errored: {e.Message}");
      }
      Console.WriteLine("Nothing to see here");
      return jsonGameList;
    }
    string UpdateReferenceDatabase(string dbase, int code)
    {
      
      return "";
    }
  }  
}

