using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using keepr.Models;
using keepr.Repositories;
using Microsoft.AspNetCore.Authorization;
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
    public ActionResult<IEnumerable<Vault>> GetAll()
    {
      return Ok(_repo.getAllVaults());
    }
    [Authorize]
    [HttpGet("user")]
    public IEnumerable<Vault> Get()
    {
      string uid = HttpContext.User.Identity.Name;
      return _repo.GetVaultByUserId(uid);
    }


    [HttpPost]
    public ActionResult<Vault> Post([FromBody] Vault value)
    {
      value.UserId = HttpContext.User.Identity.Name;
      if (value.UserId != null)
      {
        Vault result = _repo.NewVault(value);
        return Created("/api/Vault/" + result.Id, result);
      }
      return BadRequest("couldn't make vault");
    }

    [Authorize]
    [HttpDelete("{vaultId}")]
    public ActionResult<string> Delete(string vaultId)
    {
      string uid = HttpContext.User.Identity.Name;
      if (uid != null)
      {
        _repo.DeleteVault(vaultId, uid);
        return Ok("Vault was deleted!");
      }
      return BadRequest("couldn't delete Vault");
    }
  }
}