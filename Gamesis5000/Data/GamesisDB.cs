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
      
      //DropTheTableToCreateANewOne. 
      Task.Run(async () => await database.DropTableAsync<Genres>());
      //
      
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

    //TODO: Use Generics to streamline code for refreshing
    public async Task<int> RefreshDeveloper(bool fromFile = true)
    {
      Debug.WriteLine("Refreshing Developer List");
      List<Developers> devList = new List<Developers>();
      if (fromFile)
      {
        try
        {
          await database.DeleteAllAsync<Developers>();
          string json = await GetJsonStringFromFile("DeveloperJson.json");
          JObject jsonParsed = JObject.Parse(json);
          List<Developers> developerList = new List<Developers>();
          var tokenSelected = jsonParsed.SelectToken("data").SelectToken("developers")["1"]["name"];
          int lenDevelopers = jsonParsed.SelectToken("data").SelectToken("developers").Count();
          for (int i = 1; i <= lenDevelopers; i++)
          {
            //Debug.WriteLine($"[Dev Note] {i}");
            string number = i.ToString();
            try
            {
              var selectedData = jsonParsed.SelectToken("data").SelectToken("developers")[number];
            developerList.Add(new Developers
            {              
              Name = (string)selectedData["name"],
              DevId = (int)selectedData["id"]
            }); 
            }
            catch
            {              
              //Debug.WriteLine($"Entry {i} Not Found.  Skipping");
            }
          }
          await database.InsertAllAsync(developerList);
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
    public async Task<int> RefreshGenres(bool fromFile = true)
    {
      Debug.WriteLine("Refreshing Genres List");
      List<Genres> devList = new List<Genres>();
      if (fromFile)
      {
        try
        {
          await database.DeleteAllAsync<Genres>();
          string json = await GetJsonStringFromFile("GenresJson.json");
          JObject jsonParsed = JObject.Parse(json);
          List<Genres> referenceList = new List<Genres>();
          var tokenSelected = jsonParsed.SelectToken("data").SelectToken("genres")["1"]["name"];
          int len = jsonParsed.SelectToken("data").SelectToken("genres").Count();
          for (int i = 1; i <= len; i++)
          {
            Debug.WriteLine(i);
            //Debug.WriteLine($"[Dev Note] {i}");
            string number = i.ToString();
            try
            {
              var selectedData = jsonParsed.SelectToken("data").SelectToken("genres")[number];
              referenceList.Add(new Genres
              {
                Name = (string)selectedData["name"],
                GenreId = (int)selectedData["id"]
              });
            }
            catch
            {
              Debug.WriteLine($"Entry {i} Not Found.  Skipping");
            }
          }
          await database.InsertAllAsync(referenceList);
          Genres nameoftheDev = await database.Table<Genres>()
          .Where(i => i.Name == "Quiz")
          .FirstOrDefaultAsync();
          Debug.WriteLine("Proof the record exists!: " + nameoftheDev.Name);



          return await database.Table<Genres>().CountAsync();
        }
        catch (Exception e)
        {
          Debug.WriteLine($"Unable to Create Genres Object: {e.Message}");
          return -1;
        }
      }
      else
      {
        Debug.WriteLine($"Pull From the API placeholder");
        return 0;
      }
    }
    public async Task<int> RefreshGameSystem(bool fromFile = true)
    {
      Debug.WriteLine("Refreshing GameSystem List");
      if (fromFile)
      {
      await database.DeleteAllAsync<GameSystem>();
      string json = await GetJsonStringFromFile("GameSystemJson.json");
      JObject jsonParsed = JObject.Parse(json);
      List<GameSystem> referenceList = new List<GameSystem>();
      int len = jsonParsed.SelectToken("data").SelectToken("platforms").Count();
      for (int i = 1; i <= len; i++)
      { 
        string number = i.ToString();
        try
        {
          var selectedData = jsonParsed.SelectToken("data").SelectToken("platforms")[number];
          referenceList.Add(new GameSystem
          {
            //Publisher = (game["publishers"] == null ? new List<int> { -1 } : game["publishers"] 
            Name = (string)selectedData["name"],
            GsId = (int)selectedData["id"],
            Developer = (selectedData["developer"] == null ? "" : (string)selectedData["developer"]),
            Manufacturer = (selectedData["manufacturer"] == null ? "" : (string)selectedData["manufacturer"]),
            Media = (selectedData["media"] == null ? "" : (string)selectedData["media"]),
            Cpu = (selectedData["cpu"] == null ? "" : (string)selectedData["cpu"]),
            Memory = (selectedData["memory"] == null ? "" : (string)selectedData["memory"]),
            Graphics = (selectedData["graphics"] == null ? "" : (string)selectedData["graphics"]),
            Sound = (selectedData["sound"] == null ? "" : (string)selectedData["sound"]),
            MaxControllers = (selectedData["maxcontrollers"] == null ? "" : (string)selectedData["maxcontrollers"]),
            Display = (selectedData["display"] == null ? "" : (string)selectedData["display"]),
            Overview = (selectedData["overview"] == null ? "" : (string)selectedData["overview"]),
            VideoUrl = (selectedData["youtube"] == null ? "" : (string)selectedData["youtube"])
          });
        }
        catch
        {
          Debug.WriteLine($"Entry {i} Not Found.  Skipping");
        }
      }

      return referenceList;
    }
    else {
      Debug.WriteLine("PlaceHolder for API get information");
      return 0;
        }
    }
    public async Task<int> RefreshPublishers(bool fromFile = true)
    {
      Debug.WriteLine("Refreshing Publisher List");
      List<Publishers> devList = new List<Publishers>();
      if (fromFile)
      {
        try
        {
          await database.DeleteAllAsync<Publishers>();
          string json = await GetJsonStringFromFile("PublishersJson.json");
          JObject jsonParsed = JObject.Parse(json);
          List<Publishers> referenceList = new List<Publishers>();
          var tokenSelected = jsonParsed.SelectToken("data").SelectToken("publishers")["1"]["name"];
          int len = jsonParsed.SelectToken("data").SelectToken("publishers").Count();
          Debug.WriteLine($"Publisher len: {len}");
          for (int i = 1; i <= len; i++)
          {           
            string number = i.ToString();
            try
            {
              var selectedData = jsonParsed.SelectToken("data").SelectToken("publishers")[number];
              referenceList.Add(new Publishers
              {
                Name = (string)selectedData["name"],
                PubId = (int)selectedData["id"]
              });
            }
            catch
            {
             Debug.WriteLine($"Entry {i} Not Found.  Skipping");
            }
          }
          await database.InsertAllAsync(referenceList);
          Publishers nameoftheDev = await database.Table<Publishers>()
          .Where(i => i.Name == "Valve Corporation")
          .FirstOrDefaultAsync();
          Debug.WriteLine("Proof the record exists!: " + nameoftheDev.Name);
          return await database.Table<Publishers>().CountAsync();
        }
        catch (Exception e)
        {
          Debug.WriteLine($"Unable to Create Publishers Object: {e.Message}");
          return -1;
        }
      }
      else
      {
        Debug.WriteLine($"Pull From the API placeholder");
        return 0;
      }
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

