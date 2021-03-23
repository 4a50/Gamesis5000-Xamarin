using Gamesis5000.Models;
using Gamesis5000.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Gamesis5000.Data
{
  class GamesisDB : IGamesisDB
  {
    readonly SQLiteAsyncConnection database;

    public GamesisDB()
    {
      //Ensure the Database is registered as a dependency in the App.cs file.
      string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "GamesisDB.db3");
      database = new SQLiteAsyncConnection(path);
      database.CreateTableAsync<Game>().Wait();
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
  }
}
