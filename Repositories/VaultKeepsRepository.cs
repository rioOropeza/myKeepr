using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;
    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }
    //new vault keep
    public VaultKeep NewVaultKeep(VaultKeep vk)
    {
      _db.Execute(@"
        INSERT INTO vaultkeeps (vaultId, keepId, userId)
        VALUES (@VaultId, @KeepId, @UserId);
      ", vk);
      return vk;
    }
    public IEnumerable<Keep> GetVaultKeepById(int id)
    {
      return _db.Query<Keep>(@"
      SELECT * FROM vaultkeeps vk
      INNER JOIN keeps k ON k.id = vk.keepId
      WHERE(vaultId = @id) ", new { id });
    }
    public bool DeleteVaultKeep(VaultKeep vaultKeep)
    {
      int success = _db.Execute(@"
      DELETE FROM vaultkeeps WHERE keepId = @KeepId AND vaultId = @VaultId", vaultKeep);
      return success != 0;
    }
  }
}