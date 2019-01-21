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
  public class VaultsController : ControllerBase
  {
    private readonly VaultRepository _repo;

    public VaultsController(VaultRepository repo)
    {
      _repo = repo;
    }
    [HttpGet]
    public ActionResult<IEnumerable<Vault>> Get()
    {
      return Ok(_repo.getAllVaults());
    }

    [HttpGet("user/{id}")]
    public IEnumerable<Vault> Get(string id)
    {
      return _repo.GetVaultByUserId(id);
    }


    [HttpPost]
    public ActionResult<Vault> Post([FromBody] Vault value)
    {
      Vault result = _repo.NewVault(value);
      return Created("/api/Vault/" + result.Id, result);
    }
    [HttpDelete("{id}/{userid}")]
    public ActionResult<string> Delete(Vault vault)
    {
      if (_repo.DeleteVault(vault))
      {
        return Ok("Vault was deleted!");
      }
      return BadRequest("couldn't delete Vault");
    }
  }
}