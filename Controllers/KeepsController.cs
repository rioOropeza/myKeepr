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
  public class KeepsController : ControllerBase
  {
    private readonly KeepRepository _repo;

    public KeepsController(KeepRepository repo)
    {
      _repo = repo;
    }
    [HttpGet]
    public ActionResult<IEnumerable<Keep>> GetAll()
    {
      return Ok(_repo.getAllKeeps());
    }

    [HttpGet("user")]
    public IEnumerable<Keep> Get()
    {
      string uid = HttpContext.User.Identity.Name;
      return _repo.GetByUserId(uid);
    }
    [Authorize]
    [HttpPost]
    public ActionResult<Keep> Post([FromBody] Keep value)
    {
      value.UserId = HttpContext.User.Identity.Name;
      if (value.UserId != null)
      {
        Keep result = _repo.NewKeep(value);
        return Created("/api/keeps/" + result.Id, result);
      }
      return Unauthorized(value);
    }
    [Authorize]
    [HttpDelete("{keepid}")]
    public ActionResult<string> Delete(string keepId)
    {
      string uid = HttpContext.User.Identity.Name;
      if (uid != null && _repo.DeleteKeep(keepId, uid))
      {
        return Ok("keep was deleted!");
      }
      return BadRequest("couldn't delete keep");
    }

    [Authorize]
    [HttpPut("{keepId}")]
    public Keep Put(int keepId, [FromBody] Keep keep)
    {
      return _repo.editKeep(keep);
    }
  }
}
