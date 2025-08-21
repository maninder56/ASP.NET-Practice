using CommunityBoardAPI.Model.DTOs;
using CommunityBoardAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CommunityBoardAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PosterController : ControllerBase
{
    private IPosterService service; 

    public PosterController(IPosterService service)
    {
        this.service = service;
    }


    [HttpGet]
    public ActionResult<List<PosterRecord>> AllPosters()
    {
        var list = service.GetAllPosterList();

        if (list is null || list.Count == 0)
        {
            return NotFound();
        }

        return Ok(list);
    }

    [HttpGet("{id}")]
    public ActionResult<PosterDetailedRecord> GetPosterByID(int id)
    {
        var poster = service.GetPosterInDetailByID(id);

        if (poster is null)
        {
            return NotFound();
        }

        return Ok(poster);
    }

    [HttpPost]
    public ActionResult CreatePoster(NewPosterRecord poster)
    {
        var created = service.CreatePoster(poster);

        if (created is null)
        {
            return BadRequest(); 
        }

        return CreatedAtRoute(created, poster);
    }

    [HttpDelete("{id}")]
    public IActionResult DeletePosterByID(int id)
    {
        var deleted = service.DeletePosterByID(id);

        if (deleted is null)
        {
            return NotFound(); 
        }
        
        if (!(bool)deleted)
        {
            return StatusCode(500); 
        }
        else
        {
            return NoContent(); 
        }
    }

}
