using FluentValidation;
using MisGastos.Core.DTOs;
using System;

namespace MisGastos.Infrastructure.Validators
{
    public class GastoValidator : AbstractValidator<GastoCreacionDto>
    {
        public GastoValidator()
        {
            RuleFor(gasto => gasto.Descripcion)
                .NotNull()
                .WithMessage("La descripcion no puede ser nula");

            RuleFor(gasto => gasto.Descripcion)
                .Length(10, 1000)
                .WithMessage("La longitud del la descripcion debe estar entre 10 y 1000 caracteres");

            RuleFor(post => post.FechaVencimiento)
                .NotNull()
                .GreaterThanOrEqualTo(DateTime.Now)
                .WithMessage(@"La fecha de vencimiento debe ser posterior a la fecha actual");
            //.LessThan(DateTime.Now);
            //RuleFor(imp => imp.Importe)
            //    .ScalePrecision(18, 2, ignoreTrailingZeros: true);

        }
    }
}

