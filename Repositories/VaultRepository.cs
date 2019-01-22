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
    public IEnumerable<Vault> getAllVaults()
    {
      return _db.Query<Vault>("SELECT * FROM Vaults");
    }
    //get Vaults by user id
    public IEnumerable<Vault> GetVaultByUserId(string id)
    {
      return _db.Query<Vault>("SELECT * FROM Keeps WHERE userId = @id", new { id });
    }
    //create Vault
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
    //new vault keep
    public VaultKeep NewVaultKeep(VaultKeep vk)
    {
      _db.Execute(@"
        INSERT INTO vaultkeeps (vaultId, keepId, userId)
        VALUES (@VaultId, @KeepId, @UserId);
      ", vk);
      return vk;
    }
    public IEnumerable<VaultKeep> GetVaultKeepById(int id)
    {
      return _db.Query<VaultKeep>("SELECT * FROM vaultkeeps WHERE vaultid = @id", new { id });
    }
    public bool DeleteVaultKeep(VaultKeep vaultKeep)
    {
      int success = _db.Execute(@"
      DELETE FROM vaultkeeps WHERE keepId = @KeepId AND vaultId = @VaultId", vaultKeep);
      return success != 0;
    }

    //DELETE Vault
    public bool DeleteVault(string vaultId, string userId)
    {
      int success = _db.Execute(@"DELETE FROM Vaults WHERE id = @vaultId && userid = @userid", new { vaultId, userId });
      return success != 0;
    }
  }
}