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
    string longUrl = "";
    //string byTitle = 
    string jsonString = "";



    // This will be just a file access for a while until I can get the JSON parsed out correctly.    
    const string localFileName = "MetroidGamesDBResponse.json";    
    //
    public APIService()
    {
      client = new HttpClient();
      Task.Run(async () => longUrl = await EnvRead());
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
    public async Task<string> EnvRead()
    {      
      string textOutput = "";      
      using (var stream = await FileSystem.OpenAppPackageFileAsync(@"settings.txt"))
      {
        using (var reader = new StreamReader(stream))
        {
          while (textOutput != null) {
            textOutput = reader.ReadLine();
            string substring = textOutput.Substring(0, 11);
            if (textOutput.Substring(0, 11) == "GAMESDB_API")
            {
              break;
            }
          }
        }
        if (textOutput == null) { textOutput = ""; }
        else { textOutput = textOutput.Substring(13); }
      }
      //      
      return textOutput;
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
            .ToList()),            
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
    string  BoxArtUrl(JObject jsonObj, List<SearchGame> gameList)
    {
      
      return "";
    }
  }  
}

