﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidator:AbstractValidator<DeleteAuthorCommand>
    {
        //Fluent Validation
        public DeleteAuthorCommandValidator()
        {
            RuleFor(command=>command.AuthorId).GreaterThan(0);
        }
    }
}
