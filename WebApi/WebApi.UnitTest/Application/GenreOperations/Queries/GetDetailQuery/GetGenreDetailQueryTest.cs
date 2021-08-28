using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DataAccess;
using WebApi.UnitTest.TestSetup;
using Xunit;

namespace WebApi.UnitTest.Application.GenreOperations.Queries.GetDetailQuery
{
    public class GetGenreDetailQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookContext _context;
        private readonly IMapper _mapper;


        public GetGenreDetailQueryTest(CommonTestFixture testFixture)
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
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context,_mapper);
            query.GenreId  = id;
            GenreDetailQueryValidator validationRules = new GenreDetailQueryValidator();
            var result = validationRules.Validate(query);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenInvalidIdAreGiven_Book_ShouldBeReturnErrors()
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId = 999;
            FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void WhenValidIdAreGiven_Book_ShouldNotBeReturnErrors()
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId = 2;
            FluentActions.Invoking(() => query.Handle()).Should().NotThrow();
        }

    }
}
