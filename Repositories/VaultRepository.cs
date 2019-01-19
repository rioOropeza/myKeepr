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
    //get Vault by id
    public Vault getVaultById(int id)
    {
      return _db.QueryFirstOrDefault<Vault>($"SELECT * FROM Vaults WHERE id = @id", new { id });
    }
    //get Vaults by user id
    public IEnumerable<Vault> GetByUserId(int id)
    {
      return _db.Query<Vault>("SELECT * FROM Vaults WHERE userId = @id", new { id });
    }
    //create Vault
    public Vault NewVault(Vault Vault)
    {
      int id = _db.ExecuteScalar<int>(@"
      INSERT INTO Vaults(name, description, userId, img, isPrivate)
      VALUES(@name, @description, @userId, @img, @isPrivate);
      SELECT LAST_INSERT_ID();
      ", Vault);
      Vault.Id = id;
      return Vault;
    }
    //DELETE Vault
    public bool DeleteVault(int id)
    {
      int success = _db.Execute(@"DELETE FROM Vaults WHERE id = @id", new { id });
      return success != 0;
    }
  }
}