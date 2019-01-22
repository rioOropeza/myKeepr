using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class VaultRepository
  {
    private readonly IDbConnection _db;
    public VaultRepository(IDbConnection db)
    {
      _db = db;
    }
    //get all Vaults 
    public IEnumerable<Vault> getByUserId(string id)
    {
      return _db.Query<Vault>("SELECT * FROM Vaults WHERE userId =@id", new { id });
    }
    public Vault NewVault(Vault Vault)
    {
      int id = _db.ExecuteScalar<int>(@"
      INSERT INTO Vaults(name, description, userId)
      VALUES(@name, @description, @userId);
      SELECT LAST_INSERT_ID();
      ", Vault);
      Vault.Id = id;
      return Vault;
    }


    //DELETE Vault
    public bool DeleteVault(string vaultId, string userId)
    {
      int success = _db.Execute(@"DELETE FROM Vaults WHERE id = @vaultId && userid = @userid", new { vaultId, userId });
      return success != 0;
    }
  }
}