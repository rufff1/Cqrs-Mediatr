using Busines.Cqrs.Commands;
using Busines.DTOs.Blog.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busines.Validators.Blog
{
    public class BlogCreateValidator :AbstractValidator<CreateBlogCommand>
    {
        public BlogCreateValidator() 
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


            RuleFor(x => x.Name)
        .NotEmpty()
        .WithMessage("Ad daxil edilmelidir")
        .MaximumLength(20)
        .WithMessage("max chracter sayi 20 olmalidir");

            RuleFor(x => x.Author)
        .NotEmpty()
        .WithMessage("Description daxil edilmelidir")
        .MaximumLength(60)
        .WithMessage("max chracter sayi 60 olmalidir");

            RuleFor(x => x.CategoryId)
       .NotEmpty()
       .WithMessage("Ad daxil edilmelidir");

  
        }
    }
}
