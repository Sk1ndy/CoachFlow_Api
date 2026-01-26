using Microsoft.AspNetCore.Mvc;
using CoachFlowApi.Application.Interfaces;

namespace CoachFlowApi.Api.Controllers;

[ApiController] 
[Route("api/[controller]")] 
public class CoachController : ControllerBase 
{
    private readonly ICoachRepository _repository;

    public CoachController(ICoachRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCoach(Guid id)
    {
        var coach = await _repository.GetByIdAsync(id);
        return coach is not null ? Ok(coach) : NotFound();
    }
}