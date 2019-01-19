using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class KeepRepository
  {
    private readonly IDbConnection _db;
    public KeepRepository(IDbConnection db)
    {
      _db = db;
    }
    //get all keeps 
    public IEnumerable<Keep> getAllKeeps()
    {
      return _db.Query<Keep>("SELECT * FROM Keeps");
    }
    //get keep by id
    public Keep getKeepById(int id)
    {
      return _db.QueryFirstOrDefault<Keep>($"SELECT * FROM Keeps WHERE id = @id", new { id });
    }
    //get keeps by user id
    public IEnumerable<Keep> GetByUserId(int id)
    {
      return _db.Query<Keep>("SELECT * FROM Keeps WHERE userId = @id", new { id });
    }
    //create keep
    public Keep NewKeep(Keep keep)
    {
      int id = _db.ExecuteScalar<int>(@"
      INSERT INTO keeps(name, description, userId, img, isPrivate)
      VALUES(@name, @description, @userId, @img, @isPrivate);
      SELECT LAST_INSERT_ID();
      ", keep);
      keep.Id = id;
      return keep;
    }
    //DELETE KEEP
    public bool DeleteKeep(int id)
    {
      int success = _db.Execute(@"DELETE FROM keeps WHERE id = @id", new { id });
      return success != 0;
    }
  }
}