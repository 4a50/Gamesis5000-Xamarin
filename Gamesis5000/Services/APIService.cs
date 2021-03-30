using Gamesis5000.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
    string apiKey = "apikey=4c9d180c15bf5fe3e896c204472a85c752dddb4fcdf0ba291b00b037af1c1910";
    
    
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

    public async Task<string> GetSampleJSONEntry()
    {
      string textOutput;
      using (var stream = await FileSystem.OpenAppPackageFileAsync(localFileName))
      {
        using (var reader = new StreamReader(stream))
        {
          textOutput = await reader.ReadToEndAsync();
        }
      }
      return textOutput;
    }
  }
}
