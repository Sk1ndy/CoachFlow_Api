using CoachFlowApi.Application.DTOs.Auth;
using CoachFlowApi.Application.UseCases.Auth;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CoachFlowApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthController(LoginUseCase loginUseCase, RegisterUseCase registerUseCase) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        try
        {
            var result = await loginUseCase.Execute(dto);
            return Ok(result);
        }
        catch (AuthValidationException ex)
        {
            return UnprocessableEntity(new { errors = ex.Errors.Select(e => e.ErrorMessage) });
        }
        catch (InvalidCredentialsException ex)
        {
            return Unauthorized(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Une erreur interne s'est produite." });
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        try
        {
            var result = await registerUseCase.Execute(dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}