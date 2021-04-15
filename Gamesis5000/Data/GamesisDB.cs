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

    public async Task<string> GetGameSystemName(int id)
    {
      try
      {
        GameSystem gs = await database.Table<GameSystem>()
        .Where(x => x.GsId == id)
        .FirstOrDefaultAsync();
        return gs.Name;
      }
      catch
      {
        Debug.WriteLine($"Unable to Retrieve Requested GameSystem from supplied Id ({id})");
        return null;
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
              //Debug.WriteLine($"Entry {i} Not Found.  Skipping");
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
        try {
          await database.DeleteAllAsync<GameSystem>();
          string json = await GetJsonStringFromFile("GameSystemJson.json");
          JObject jsonParsed = JObject.Parse(json);
          List<GameSystem> referenceList = new List<GameSystem>();
          
          var allJson = jsonParsed
            .SelectToken("data")
            .SelectToken("platforms")
            .ToArray();
          var idOfFirstEntry = (string)allJson[0].Values().ToList()[1];
          
          for (int i = 1; i <= allJson.Length; i++)
          {
            try
            {
            var gameSysEntry = allJson[i].Values().ToList();
              referenceList.Add(new GameSystem
              {
                //Publisher = (game["publishers"] == null ? new List<int> { -1 } : game["publishers"] 
                Name = (string)gameSysEntry[1],
                GsId = (int)gameSysEntry[0],
                Developer = ((string)gameSysEntry[6] == null ? "" : (string)gameSysEntry[6]),
                Manufacturer = ((string)gameSysEntry[7] == null ? "" : (string)gameSysEntry[7]),
                Media = ((string)gameSysEntry[8] == null ? "" : (string)gameSysEntry[8]),
                Cpu = ((string)gameSysEntry[9] == null ? "" : (string)gameSysEntry[9]),
                Memory = (gameSysEntry[10] == null ? "" : (string)gameSysEntry[10]),
                Graphics = (gameSysEntry[11] == null ? "" : (string)gameSysEntry[11]),
                Sound = (gameSysEntry[12] == null ? "" : (string)gameSysEntry[12]),
                MaxControllers = (gameSysEntry[13] == null ? "" : (string)gameSysEntry[13]),
                Display = (gameSysEntry[14] == null ? "" : (string)gameSysEntry[14]),
                Overview = (gameSysEntry[15] == null ? "" : (string)gameSysEntry[15]),
                VideoUrl = (gameSysEntry[16] == null ? "" : (string)gameSysEntry[16])
              });
            }
            catch
            {
              //Debug.WriteLine($"Entry {i} Not Found.  Skipping");
            }
          }
          await database.InsertAllAsync(referenceList);
          GameSystem nameoftheGameSys = await database.Table<GameSystem>()
          .Where(i => i.Name == "Nintendo Entertainment System (NES)")
          .FirstOrDefaultAsync();
          Debug.WriteLine("Proof the record exists!: " + nameoftheGameSys.Name);
          return await database.Table<GameSystem>().CountAsync();
        }
        catch (Exception e)
        {
          Debug.WriteLine($"Unable to Create GameSystem Object: {e.Message}");
          return -1;
        }
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
             //Debug.WriteLine($"Entry {i} Not Found.  Skipping");
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
          Developer = "Konami",
          Publisher = "Nintendo",
          VideoUrl = ""
        });

      }
    }
  }
  
 
}

