using FluentValidation;
using ConcesionariaBackend.DTOs;

namespace ConcesionariaBackend.Validations
{
    public class FacturaDTOValidator : AbstractValidator<FacturaDTO>
    {
        public FacturaDTOValidator()
        {
            RuleFor(x => x.NumeroFactura)
                .NotEmpty().WithMessage("El número de factura es obligatorio.")
                .MaximumLength(50).WithMessage("No puede superar los 50 caracteres.");

            RuleFor(x => x.Fecha)
                .NotEmpty().WithMessage("La fecha es obligatoria.");

            RuleFor(x => x.Total)
                .NotNull().WithMessage("El total es obligatorio.")
                .GreaterThan(0).WithMessage("El total debe ser mayor a cero.");

            RuleFor(x => x.TipoFactura)
                .NotEmpty().WithMessage("El tipo de factura es obligatorio.")
                .Must(t => new[] { "A", "B", "C" }.Contains(t))
                .WithMessage("El tipo de factura debe ser 'A', 'B' o 'C'.");

            RuleFor(x => x.CuitCliente)
                .MaximumLength(20).WithMessage("El CUIT no puede superar los 20 caracteres.")
                .When(x => !string.IsNullOrWhiteSpace(x.CuitCliente));

            RuleFor(x => x.RazonSocialCliente)
                .MaximumLength(100).WithMessage("La razón social no puede superar los 100 caracteres.")
                .When(x => !string.IsNullOrWhiteSpace(x.RazonSocialCliente));

            RuleFor(x => x.VentaId)
                .GreaterThan(0).WithMessage("Debe especificar el ID de la venta asociada.");
        }
    }
}



