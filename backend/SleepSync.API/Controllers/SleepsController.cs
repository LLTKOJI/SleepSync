using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SleepSync.Application.Interfaces;
using SleepSync.Domain.Entities;
using SleepSync.Application.Features.Sleeps.DTOs;

namespace SleepSync.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SleepsController : ControllerBase
{
    private readonly ISleepRepository _repository;
    private readonly IValidator<CreateSleepRequestDto> _validator;

    public SleepsController(ISleepRepository repository, IValidator<CreateSleepRequestDto> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> GetSleeps()
    {
        var sleeps = await _repository.GetAllAsync();

        // Mapeo Manual: Entidad -> DTO
        var response = sleeps.Select(s => new SleepResponseDto
        {
            Id = s.Id,
            Nombre = s.Nombre
        });

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CrearSleep([FromBody] CreateSleepRequestDto request)
    {
        // 1. Ejecutar FluentValidation
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors); // HTTP 400 automático con detalles
        }

        // 2. Mapeo Manual: DTO -> Entidad (Solo transferimos lo permitido)
        var nuevoSleep = new SleepGlobal
        {
            Nombre = request.Nombre
        };

        // 3. Persistencia
        var sleepCreado = await _repository.AddAsync(nuevoSleep);

        // 4. Mapeo de Retorno
        var response = new SleepResponseDto
        {
            Id = sleepCreado.Id,
            Nombre = sleepCreado.Nombre
        };

        return CreatedAtAction(nameof(GetSleeps), new { id = response.Id }, response);
    }
}