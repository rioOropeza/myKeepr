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
    private readonly VaultKeepsRepository _repo;

    public VaultKeepsController(VaultKeepsRepository repo)
    {
      _repo = repo;
    }

    [HttpPost]
    public ActionResult<VaultKeep> Post([FromBody] VaultKeep value)
    {
      value.UserId = HttpContext.User.Identity.Name;
      if (value.UserId != null)
      {
        VaultKeep result = _repo.NewVaultKeep(value);
        return Created("good", result);
      }
      return BadRequest("can't make vaultkeep");
    }

    [HttpGet("{id}")]
    public IEnumerable<Keep> Get(int id)
    {

      return _repo.GetVaultKeepById(id);

    }
    [HttpPut]
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