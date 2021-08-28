using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.BookOperations.CreateBook
{
   
    public class CreateBookCommandValidator:AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
           
            RuleFor(cmd=>cmd.Model.GenreId).GreaterThan(0).WithMessage("GenreId 0 dan küçük veya 0 Olamaz");
            RuleFor(cmd=>cmd.Model.PageCount).GreaterThan(0);

            RuleFor(cmd=>cmd.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
            
            
            RuleFor(cmd => cmd.Model.Title).NotEmpty().MinimumLength(4);
        }
    }
}
