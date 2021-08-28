using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator:AbstractValidator<CreateAuthorCommand>
    {
        // Fluent  Validation RuleFor
        public CreateAuthorCommandValidator()
        {
            RuleFor(command=>command.Model.Name).NotEmpty().MinimumLength(3);
            RuleFor(command=>command.Model.Surname).NotEmpty().MinimumLength(3);    
            RuleFor(command => command.Model.BirthDate.Date).NotEmpty().LessThan(DateTime.Now.Date.AddYears(-10));
        }
    }
}
