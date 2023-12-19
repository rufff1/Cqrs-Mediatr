using Busines.Cqrs.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busines.Validators.Category
{
    public class CategoryCreateValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CategoryCreateValidator()
        {
            RuleFor(x => x.Name)
        .NotEmpty()
        .WithMessage("Ad daxil edilmelidir")
        .MaximumLength(20)
        .WithMessage("max chracter sayi 20 olmalidir");

            RuleFor(x => x.Description)
        .NotEmpty()
        .WithMessage("Description daxil edilmelidir")
        .MaximumLength(200)
        .WithMessage("max chracter sayi 200 olmalidir");
        }
    }
}
