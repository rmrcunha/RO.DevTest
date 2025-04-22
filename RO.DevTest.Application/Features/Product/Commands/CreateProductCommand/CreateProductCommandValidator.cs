using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.Product.Commands.CreateProductCommand;
public class CreateProductCommandValidator:AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(cpau => cpau.Name)
        .NotNull()
        .NotEmpty()
        .WithMessage("O campo nome precisa ser preenchido");

        RuleFor(cpau => cpau.Price)
            .NotNull()
            .NotEmpty()
            .WithMessage("O campo preço precisa ser preenchido")
            .GreaterThan(0)
            .WithMessage("O campo preço precisa ser maior que 0");

        RuleFor(cpau => cpau.Quantity)
            .NotNull()
            .NotEmpty()
            .WithMessage("O campo quantidade precisa ser preenchido")
            .GreaterThan(0)
            .WithMessage("O campo quantidade precisa ser maior que 0");

        RuleFor(cpau => cpau.Description)
            .NotNull()
            .NotEmpty()
            .WithMessage("O campo descrição precisa ser preenchido");
    }

}
