using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SleepSync.Application.Interfaces;
using SleepSync.Domain.Entities;
using SleepSync.Application.Features.Sleeps.DTOs;

namespace SleepSync.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SleepsController : ControllerBase {
    private readonly ISleepRepository _repository;
    private readonly IValidator<CreateSleepRequestDto> _createValidator;
    private readonly IValidator<UpdateSleepRequestDto> _updateValidator;

    public SleepsController(
        ISleepRepository repository,
        IValidator<CreateSleepRequestDto> createValidator,
        IValidator<UpdateSleepRequestDto> updateValidator) {
        _repository = repository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    [HttpGet]
    public async Task<IActionResult> GetSleeps() {
        var sleeps = await _repository.GetAllAsync();

        var response = sleeps.Select(s => new SleepResponseDto {
            Id = s.Id,
            Nombre = s.Nombre
        });

        return Ok(response);
    }

    // GET /api/sleeps/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSleepById(int id) {
        var sleep = await _repository.GetByIdAsync(id);
        if (sleep is null) {
            return NotFound(); // HTTP 404
        }

        var response = new SleepResponseDto {
            Id = sleep.Id,
            Nombre = sleep.Nombre
        };

        return Ok(response); // HTTP 200
    }

    [HttpPost]
    public async Task<IActionResult> CrearSleep([FromBody] CreateSleepRequestDto request) {
        var validationResult = await _createValidator.ValidateAsync(request);
        if (!validationResult.IsValid) {
            return BadRequest(validationResult.Errors);
        }

        var nuevoSleep = new SleepGlobal {
            Nombre = request.Nombre
        };

        var sleepCreado = await _repository.AddAsync(nuevoSleep);

        var response = new SleepResponseDto {
            Id = sleepCreado.Id,
            Nombre = sleepCreado.Nombre
        };

        return CreatedAtAction(nameof(GetSleepById), new { id = response.Id }, response);
    }

    // PUT /api/sleeps/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarSleep(int id, [FromBody] UpdateSleepRequestDto request) {
        var validationResult = await _updateValidator.ValidateAsync(request);
        if (!validationResult.IsValid) {
            return BadRequest(validationResult.Errors); // HTTP 400
        }

        var sleep = await _repository.GetByIdAsync(id);
        if (sleep is null) {
            return NotFound(); // HTTP 404
        }

        sleep.Nombre = request.Nombre;
        await _repository.UpdateAsync(sleep);

        return NoContent(); // HTTP 204
    }

    // DELETE /api/sleeps/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarSleep(int id) {
        var eliminado = await _repository.DeleteAsync(id);
        if (!eliminado) {
            return NotFound(); // HTTP 404
        }

        return NoContent(); // HTTP 204
    }
}