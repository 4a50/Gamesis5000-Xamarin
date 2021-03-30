using Gamesis5000.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gamesis5000.Services
{
  public class APIService
  {
    HttpClient client;
    //"https://api.thegamesdb.net/v1/Games/ByGameName?apikey=4c9d180c15bf5fe3e896c204472a85c752dddb4fcdf0ba291b00b037af1c1910&name=super%20Metroid&filter%5Bplatform%5D=6"
    string BaseUrl = "https://api.thegamesdb.net/v1/Games/ByGameName?apikey=4c9d180c15bf5fe3e896c204472a85c752dddb4fcdf0ba291b00b037af1c1910&name=super%20Metroid&filter%5Bplatform%5D=6";
    //string byTitle = 
    string apiKey = "apikey=4c9d180c15bf5fe3e896c204472a85c752dddb4fcdf0ba291b00b037af1c1910";
    public APIService()
    {
      client = new HttpClient();
    }

    public async Task<Game> GetDataAsync()
    {
      //Uri uri = new Uri(;
      return new Game();
    }
  }
}
