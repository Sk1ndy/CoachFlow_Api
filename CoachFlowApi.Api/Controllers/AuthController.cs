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
    [Authorize]
    public async Task<IActionResult> Me()
    {
        var idClaim = User.FindFirst("sub") ?? User.FindFirst(ClaimTypes.NameIdentifier);

        if (idClaim == null || string.IsNullOrWhiteSpace(idClaim.Value))
            return Unauthorized(new { error = "Token valide, mais l'ID utilisateur est introuvable dans les claims." });

        if (!int.TryParse(idClaim.Value, out int userId))
            return Unauthorized(new { error = "Format de l'ID utilisateur invalide dans le token." });

        try
        {
            var result = await getUserInfoUseCase.Execute(userId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

}