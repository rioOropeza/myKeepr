using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using keepr.Models;
using keepr.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultRepository _repo;

    public VaultKeepsController(VaultRepository repo)
    {
      _repo = repo;
    }
    [HttpPut]
    public string Put([FromBody] VaultKeep vaultKeep)
    {
      if (!ModelState.IsValid)
      {
        return "can't create vault keep";
      }
      return _repo.NewVaultKeep(vaultKeep);
    }
  }
}