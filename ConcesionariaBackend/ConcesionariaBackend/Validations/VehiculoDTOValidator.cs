using FluentValidation;
using ConcesionariaBackend.DTOs;

public class VehiculoDTOValidator : AbstractValidator<VehiculoDTO>
{
    public VehiculoDTOValidator()
    {
        RuleFor(v => v.Marca).NotEmpty();
        RuleFor(v => v.Modelo).NotEmpty();
        RuleFor(v => v.Anio).InclusiveBetween(1900, DateTime.Now.Year);
        RuleFor(v => v.Precio).GreaterThan(0);
    }
}

