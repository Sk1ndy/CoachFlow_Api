using CoachFlowApi.Application.DTOs.Auth;
using CoachFlowApi.Application.Exceptions;
using CoachFlowApi.Application.UseCases.Auth;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace CoachFlowApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthController(LoginUseCase loginUseCase, RegisterUseCase registerUseCase, GetUserInfoUseCase getUserInfoUseCase) : ControllerBase
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
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        try
        {
            var result = await registerUseCase.Execute(dto);
            return Created(string.Empty, result);
        }
        catch (AuthValidationException ex)
        {
            return UnprocessableEntity(new { errors = ex.Errors.Select(e => e.ErrorMessage) });
        }
        catch (UserAlreadyExistsException ex)
        {
            return Conflict(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }


[HttpGet("me")]
public async Task<IActionResult> Me()
{
    var sub = User.FindFirst("sub")?.Value;
    if (string.IsNullOrWhiteSpace(sub))
        return Unauthorized("Token invalide ou manquant");

    var userId = int.Parse(sub);
    var result = await getUserInfoUseCase.Execute(userId);
    return Ok(result);
}


}