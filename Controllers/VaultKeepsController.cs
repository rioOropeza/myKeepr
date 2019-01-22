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
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultRepository _repo;

    public VaultKeepsController(VaultRepository repo)
    {
      _repo = repo;
    }
    [Authorize]
    [HttpPost]
    public ActionResult<VaultKeep> Post([FromBody] VaultKeep value)
    {
      value.userId = HttpContext.User.Identity.Name;
      if (value.userId != null)
      {
        VaultKeep result = _repo.NewVaultKeep(value);
        return Created("good", result);
      }
      return BadRequest("can't make vaultkeep");
    }
    [HttpGet("{id}")]
    public IEnumerable<VaultKeep> Get(int id)
    {
      return _repo.GetVaultKeepById(id);
    }
    [HttpDelete]
    public ActionResult<string> DeleteVaultKeep([FromBody]VaultKeep vaultKeep)
    {
      var result = _repo.DeleteVaultKeep(vaultKeep);
      if (result != false)
      {
        return Ok("deleted vault keep");
      }
      return BadRequest("not deleted");
    }
  }
}