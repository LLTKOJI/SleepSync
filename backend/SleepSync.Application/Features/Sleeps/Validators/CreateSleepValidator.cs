using FluentValidation;
using SleepSync.Application.Features.Sleeps.DTOs;

namespace SleepSync.Application.Features.Sleeps.Validators;

public class CreateSleepValidator : AbstractValidator<CreateSleepRequestDto>
{
    public CreateSleepValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre no puede estar vacío.")
            .MinimumLength(3).WithMessage("Debe tener al menos 3 caracteres.")
            .MaximumLength(100).WithMessage("El texto es demasiado largo.");
    }
}