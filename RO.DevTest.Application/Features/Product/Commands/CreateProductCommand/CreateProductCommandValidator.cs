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

        RuleFor(cpau => cpau.Description)
            .NotNull()
            .NotEmpty()
            .WithMessage("O campo descrição precisa ser preenchido");
    }

}
