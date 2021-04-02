using Gamesis5000.Models;
using Gamesis5000.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Gamesis5000.Data
{
  class GamesisDB : IGamesisDB<Game>
  {
    readonly SQLiteAsyncConnection database;

    public GamesisDB()
    {
      //Ensure the Database is registered as a dependency in the App.cs file.
      string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "GamesisDB.db3");
      database = new SQLiteAsyncConnection(path);
      database.CreateTableAsync<Game>().Wait();
      database.CreateTableAsync<Developers>().Wait();
      database.CreateTableAsync<GameSystem>().Wait();
      database.CreateTableAsync<Genres>().Wait();
      database.CreateTableAsync<Publishers>().Wait();

      SeedGameData();
    }
    public async Task<bool> AddGameAsync(Game game)
    {
      try
      {
        await database.InsertAsync(game);

        return true;
      }
      catch {

        return false;
      }
    }
    public async Task<bool> DatabaseMasterReset()
    {
      try
      {
        await database.DeleteAllAsync<Game>();
        return true;
      }
      catch
      {
        return false;
      }
    }
    public async Task<bool> DeleteGameAsync(int ID)
    {
      try
      {
        await database.DeleteAsync(ID);
        return true;
      }
      catch
      {
        return false;
      }
    }
    public async Task<List<Game>> GetAllGamesAsync()
    {
      try
      {
        List<Game> games = await database.Table<Game>().ToListAsync();
        return games;
      }
      catch
      {
        Debug.WriteLine("Unable to Retrieve a list of GAMES from Database");
        return null;
      }
    }
    public async Task<Game> GetGameASync(int ID)
    {
      try
      {
        Game game = await database.Table<Game>()
          .Where(i => i.ID == ID)
          .FirstOrDefaultAsync();
        return game;
      }
      catch
      {
        Debug.WriteLine("Unable to retrieve requested GAME item");
        return null;
      }
    }
    public async Task<bool> UpdateGameAsync(Game game)
    {
      try
      {
        await database.UpdateAsync(game);
        return true;
      }
      catch
      {
        return false;
      }
    }
    public async void SeedGameData()
    {
      Debug.WriteLine("[Dev Note] Seeding Data Method Executing");
      var data = await GetAllGamesAsync();
      if (data == null || data.Count == 0)
      {
        Debug.WriteLine("[Dev Note] Seeding Data to Database");
        await AddGameAsync(new Game
        {
          Name = "Super Mario World",
          Description = "Launch Title for the SNES",
          GameSystem = "SNES",
          Genre = "Platformer",
          ReleaseDate = DateTime.Parse("November 1990"),
          BoxArtUrl = "",
          Developer = "Nintendo",
          Publisher = "Nintendo",
          VideoUrl = ""

        });

        await AddGameAsync(new Game
        {
          Name = "Castlevania",
          Description = "First in the franchise chronicling the quest of Simon Belmont against Count Dracula",
          GameSystem = "NES",
          Genre = "Platformer",
          ReleaseDate = DateTime.Parse("September 1986"),
          BoxArtUrl = "",
          Developer = "Konami",
          Publisher = "Nintendo",
          VideoUrl = ""
        });

      }
    }

    public async Task<int> RefreshDeveloper(bool fromFile = true)
    {
      List<Developers> devList = new List<Developers>();
      if (fromFile)
      {
        try
        {
          await database.DeleteAllAsync<Developers>();
          string json = await GetJsonStringFromFile("DeveloperJson.json");
          JObject jsonParsed = JObject.Parse(json);

          var tokenSelected = jsonParsed.SelectToken("data").SelectToken("developers")["1"]["name"];
          int lenDevelopers = jsonParsed.SelectToken("data").SelectToken("developers").Count();
          for (int i = 1; i <= lenDevelopers; i++)
          {
            string number = i.ToString();
            await database.InsertAsync(new Developers
            {
              Name = (string)jsonParsed.SelectToken("data").SelectToken("developers")[number]["name"],
              DevId = (int)jsonParsed.SelectToken("data").SelectToken("developers")[number]["id"]
            });
          }

          Developers nameoftheDev = await database.Table<Developers>()
          .Where(i => i.Name == "1001 Software Development")
          .FirstOrDefaultAsync();
          Debug.WriteLine("Proof the record exists!: " + nameoftheDev.Name);
          
            

          return await database.Table<Developers>().CountAsync();
        }
        catch (Exception e) {
          Debug.WriteLine($"Unable to Create Developers Object: {e.Message}");
          return -1;
        }
      }
      else {
        Debug.WriteLine($"Pull From the API placeholder");
        return 0;
      }
    }


    public Task<bool> RefreshGameSystemTable(bool fromFile = true)
    {
      throw new NotImplementedException();
    }

    public Task<bool> RefreshGenresTable(bool fromFile = true)
    {
      throw new NotImplementedException();
    }

    public Task<bool> RefreshPublishersTable(bool fromFile = true)
    {
      throw new NotImplementedException();
    }

    public async Task<string> GetJsonStringFromFile(string fileName)
    {
      string stringOut = "";
      using (var stream = await FileSystem.OpenAppPackageFileAsync(fileName))
      {
        using (var reader = new StreamReader(stream))
        {
          stringOut = reader.ReadToEnd();
        }
      }
      return stringOut;
    }
  }
  
 
}

