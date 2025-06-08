using FluentValidation;
using ConcesionariaBackend.DTOs;

public class VentaDTOValidator : AbstractValidator<VentaDTO>
{
    public VentaDTOValidator()
    {
        RuleFor(v => v.ClienteId).GreaterThan(0);
        RuleFor(v => v.VehiculoId).GreaterThan(0);
        RuleFor(v => v.FechaVenta).LessThanOrEqualTo(DateTime.Now);
    }
}

