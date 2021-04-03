﻿using Gamesis5000.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gamesis5000.Services
{
  public interface IGamesisDB<T>
  {
    Task<bool> AddGameAsync(Game game);
    Task<Game> GetGameASync(int ID);
    Task<List<Game>> GetAllGamesAsync();
    Task<bool> UpdateGameAsync(Game game);
    Task<bool> DeleteGameAsync(int ID);
    Task<bool> DatabaseMasterReset();
    Task<int> RefreshDeveloper(bool fromFile);
    Task<int> RefreshGameSystemTable(bool fromFile);
    Task<int> RefreshGenres(bool fromFile);
    Task<int> RefreshPublishers(bool fromFile);
    Task<string> GetJsonStringFromFile(string fileType);
  }
}
