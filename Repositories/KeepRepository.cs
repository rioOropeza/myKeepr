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
      return _db.Query<Keep>("SELECT * FROM Keeps WHERE isPrivate=false");
    }
    //get keeps by user id
    public IEnumerable<Keep> GetByUserId(string id)
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
    public bool DeleteKeep(string keepId, string userid)
    {
      int success = _db.Execute(@"DELETE FROM Keeps WHERE id = @keepId && userid = @userid", new { keepId, userid });
      return success != 0;
    }
    //get keeps by vault ID
    public IEnumerable<Keep> getKeepsByVaultId(int id)
    {
      return _db.Query<Keep>(@"SELECT * FROM vaultkeeps vk
INNER JOIN keeps k ON k.id = vk.keepId
WHERE(vaultId = @vaultId AND vk.userId = @userId)");
    }
    public Keep editKeep(Keep keep)
    {
      _db.Execute(@"
      UPDATE keeps SET 
        name = @Name, 
        img = @Img, 
        description = @Description, 
        isPrivate = @IsPrivate, 
        keeps=@keeps,
        views=@views,
        shares=@shares
        WHERE id = @Id
      ", keep);
      return keep;
    }
  }
}