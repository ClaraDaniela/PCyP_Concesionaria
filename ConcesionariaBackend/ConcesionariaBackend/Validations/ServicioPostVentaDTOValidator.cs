using FluentValidation;
using ConcesionariaBackend.DTOs;

public class ServicioPostVentaDTOValidator : AbstractValidator<ServicioPostVentaDTO>
{
    public ServicioPostVentaDTOValidator()
    {
        RuleFor(s => s.Descripcion).NotEmpty().MaximumLength(255);
        RuleFor(s => s.FechaSolicitud).LessThanOrEqualTo(DateTime.Now);
        RuleFor(s => s.ClienteId).GreaterThan(0);
        RuleFor(s => s.VehiculoId).GreaterThan(0);
    }
}

