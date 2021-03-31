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
    //"https://api.thegamesdb.net/v1/Games/ByGameName?apikey=4c9d180c15bf5fe3e896c204472a85c752dddb4fcdf0ba291b00b037af1c1910&name=super%20Metroid&filter%5Bplatform%5D=6"
    string BaseUrl = "https://api.thegamesdb.net/v1/Games/ByGameName?apikey=4c9d180c15bf5fe3e896c204472a85c752dddb4fcdf0ba291b00b037af1c1910&name=super%20Metroid&filter%5Bplatform%5D=6";
    //string byTitle = 
    


    // This will be just a file access for a while until I can get the JSON parsed out correctly.    
    const string localFileName = "SMB3GamesDBResponse.json";
    string localPath;
    //

    public APIService()
    {
      client = new HttpClient();
    }

    public async Task<Game> GetDataAsync()
    {
      //Uri uri = new Uri(;
      return new Game();
    }

    public async Task<List<SearchGame>> InportFileConvertToSearchGame()
    {
      //Local File.  Do Not use when accessing actual API
      string textOutput;
      using (var stream = await FileSystem.OpenAppPackageFileAsync(localFileName))
      {
        using (var reader = new StreamReader(stream))
        {
          textOutput = await reader.ReadToEndAsync();
        }
      }
      //
      List<SearchGame> searchGame = JsonToSearchGameList(textOutput);
      return searchGame;
    }

    public List<SearchGame> JsonToSearchGameList(string jsonString)
    {
      List<SearchGame> gameList = new List<SearchGame>();
      JObject jsonObj = JObject.Parse(jsonString);
      List<SearchGame> jsonGameList = jsonObj["data"]["games"]
        .Select(game => new SearchGame
        {
          Name = (string)game["game_title"],
          TitleId = (int)game["id"],
          ReleaseDate = DateTime.Parse((string)game["release_date"]),
          Genre = game["genres"].Select(g => (int)g).ToList(),
          GameSystem = (int)game["platform"],
          Description = (string)game["overview"],
          Developer = game["developers"].Select(d => (int)d).ToList(),
          Publisher = game["publishers"].Select(p => (int)p).ToList()
        }).ToList();
      Console.WriteLine("Nothing to see here");
      return jsonGameList;
    }
   
  }
  public class JsonGames
  {
    public int id { get; set; }
    public string game_title { get; set; }
    public string release_date { get; set; }
    string platform { get; set; }
    string[] developers { get; set; }
    string[] genres { get; set; }
  }
}

