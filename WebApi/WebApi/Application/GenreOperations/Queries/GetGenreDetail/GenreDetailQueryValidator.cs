using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GenreDetailQueryValidator:AbstractValidator<GetGenreDetailQuery>
    {
        public GenreDetailQueryValidator()
        {
            RuleFor(query=> query.GenreId).GreaterThan(0);
        }
    }
}
