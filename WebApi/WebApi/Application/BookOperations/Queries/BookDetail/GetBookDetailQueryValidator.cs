using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.BookOperations.BookDetail;

namespace WebApi.Application.BookOperations.Queries.BookDetail
{
    public class GetBookDetailQueryValidator:AbstractValidator<GetBookDetailQuery>
    {
        public GetBookDetailQueryValidator()
        {
            RuleFor(query => query.BookId).GreaterThan(0);
        }
    }
}
