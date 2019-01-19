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
  public class KeepsController : ControllerBase
  {
    private readonly KeepRepository _repo;

    public KeepsController(KeepRepository repo)
    {
      _repo = repo;
    }
    [HttpGet]
    public ActionResult<IEnumerable<Keep>> Get()
    {
      return Ok(_repo.getAllKeeps());
    }

    [HttpGet("{id}")]
    public ActionResult<Keep> Get(int id)
    {
      Keep result = _repo.getKeepById(id);
      if (result != null)
      {
        return Ok(result);
      }
      return BadRequest();
    }

    [HttpPost]
    public ActionResult<Keep> Post([FromBody] Keep value)
    {
      Keep result = _repo.NewKeep(value);
      return Created("/api/keeps/" + result.Id, result);
    }
    [HttpDelete("{id}")]
    public ActionResult<string> Delete(int id)
    {
      if (_repo.DeleteKeep(id))
      {
        return Ok("keep was deleted!");
      }
      return BadRequest("couldn't delete keep");
    }
  }
}