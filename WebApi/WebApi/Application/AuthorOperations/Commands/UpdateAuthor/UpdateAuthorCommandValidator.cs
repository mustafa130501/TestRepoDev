using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator:AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(3).When(x=>x.Model.Name!=string.Empty);
            RuleFor(command => command.Model.Surname).NotEmpty().MinimumLength(3).When(x => x.Model.Surname != string.Empty); ;
            RuleFor(command => command.Model.BirthDate.Date).LessThan(DateTime.Now.Date.AddYears(-10)).When(x=>x.Model.BirthDate.Date!=DateTime.Now.Date);
        }
    }
}
