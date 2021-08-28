using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DataAccess;
using WebApi.UnitTest.TestSetup;
using Xunit;

namespace WebApi.UnitTest.Application.GenreOperations.Commands.DeleteCommand
{
    public class DeleteGenreCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookContext _context;
        private readonly IMapper _mapper;

        public DeleteGenreCommandTest(CommonTestFixture testFixture)
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
            DeleteGenreCommand command = new DeleteGenreCommand(_context, _mapper);
            command.GenreId = id;
            DeleteGenreCommandValidator validationRules = new DeleteGenreCommandValidator();
            var result = validationRules.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenInvalidIdAreGiven_Book_ShouldBeReturnErrors()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context, _mapper);
            command.GenreId = 999;
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void WhenValidIdAreGiven_Book_ShouldNotBeReturnErrors()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context, _mapper);
            command.GenreId = 1;
            FluentActions.Invoking(() => command.Handle()).Should().NotThrow();
        }
    }
}
