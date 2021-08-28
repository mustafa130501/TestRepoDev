using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.BookOperations.DeleteBook;
using WebApi.DataAccess;
using WebApi.UnitTest.TestSetup;
using Xunit;

namespace WebApi.UnitTest.Application.BookOperations.Commands.DeleteCommand
{
    public class DeleteBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookContext _context;

        public DeleteBookCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }


        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(null)]
        public void WhenInvalidIdAreGiven_Validator_ShouldBeReturnErrors(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = id;
            DeleteBookCommandValidator validationRules = new DeleteBookCommandValidator();
            var result = validationRules.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenInvalidIdAreGiven_Book_ShouldBeReturnErrors()
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = 999; 
            FluentActions.Invoking(()=>command.Handle()).Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void WhenValidIdAreGiven_Book_ShouldNotBeReturnErrors()
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = 1;
            FluentActions.Invoking(() => command.Handle()).Should().NotThrow();
        }
    }
}
