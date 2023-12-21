using Busines.Cqrs.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busines.Validators.Tag
{
    public class TagUpdateValidator :AbstractValidator<UpdateTagCommand>
    {
        public TagUpdateValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Name daxil edilmelidir")
                .MaximumLength(20)
                .WithMessage("20 herfden cox ola bilmez");
        }
    }
}
