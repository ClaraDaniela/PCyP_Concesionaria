using FluentValidation;
using ConcesionariaBackend.DTOs;

public class ClienteDTOValidator : AbstractValidator<ClienteDTO>
{
    public ClienteDTOValidator()
    {
        RuleFor(c => c.Nombre).NotEmpty().WithMessage("El nombre es obligatorio.");
        RuleFor(c => c.Apellido).NotEmpty().WithMessage("El apellido es obligatorio.");
        RuleFor(c => c.Email).NotEmpty().EmailAddress().WithMessage("Debe ser un email válido.");
        RuleFor(c => c.Telefono).MaximumLength(20);
    }
}

