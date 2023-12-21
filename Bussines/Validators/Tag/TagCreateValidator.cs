using Busines.Cqrs.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busines.Validators.Tag
{
    public class TagCreateValidator : AbstractValidator<CreateTagCommand>
    {
        public TagCreateValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Name daxil edilmelidir")
                .MaximumLength(20)
                .WithMessage("max 20 herf daxil edin");

        }
    }
}
