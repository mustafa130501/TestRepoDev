using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.BookOperations.Queries.BookDetail;
using WebApi.BookOperations.BookDetail;
using WebApi.DataAccess;
using WebApi.UnitTest.TestSetup;
using Xunit;

namespace WebApi.UnitTest.Application.BookOperations.Queries
{
    public class GetBookDetailQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailQueryTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(null)]
        public void WhenInvalidIdAreGiven_Validator_ShouldBeReturnErrors(int id)
        {
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = id;
            GetBookDetailQueryValidator validationRules = new GetBookDetailQueryValidator();
            var result = validationRules.Validate(query);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenInvalidIdAreGiven_Book_ShouldBeReturnErrors()
        {
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = 999;
            FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void WhenValidIdAreGiven_Book_ShouldNotBeReturnErrors()
        {
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = 1;
            FluentActions.Invoking(() => query.Handle()).Should().NotThrow();
        }

    }
}
