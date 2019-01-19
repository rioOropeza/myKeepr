using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using keepr.Models;
using keepr.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Vaultr.Controllers
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

    [HttpGet("{id}")]
    public ActionResult<Vault> Get(int id)
    {
      Vault result = _repo.getVaultById(id);
      if (result != null)
      {
        return Ok(result);
      }
      return BadRequest();
    }

    [HttpPost]
    public ActionResult<Vault> Post([FromBody] Vault value)
    {
      Vault result = _repo.NewVault(value);
      return Created("/api/Vault/" + result.Id, result);
    }
    [HttpDelete("{id}")]
    public ActionResult<string> Delete(int id)
    {
      if (_repo.DeleteVault(id))
      {
        return Ok("Vault was deleted!");
      }
      return BadRequest("couldn't delete Vault");
    }
  }
}