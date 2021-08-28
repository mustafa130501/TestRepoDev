using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DataAccess;
using WebApi.Entities;
using WebApi.UnitTest.TestSetup;
using Xunit;

namespace WebApi.UnitTest.Application.BookOperations.Commands.UpdateCommand
{
    public class UpdateBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookContext _context;
        private readonly IMapper _mapper;

        public UpdateBookCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Theory]
        [InlineData(0,"",1,0)]
        [InlineData(0, "Lor", 100, 1)]
        [InlineData(0, "Lord", 0, 1)]
        [InlineData(1, "", 1, 0)]
        [InlineData(1, "Lor", 100, 1)]
        [InlineData(1, "Lord", 0, 1)]
        //[InlineData(1, "Lord", 100, 1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int id,string title, int pageCount, int genreId)
        {

            //arrange
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = id;
            command.Model = new UpdateBookModel
            {
                
                Title = title,
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date.AddYears(-5),
                GenreId = genreId,
                
            };

            //act
            UpdateBookCommandValidator validationRules = new UpdateBookCommandValidator();
            var result = validationRules.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = 1;
            command.Model = new UpdateBookModel
            {
                Title = "Lord Of The Rings",
                PageCount = 100,
                PublishDate = DateTime.Now.Date,
                GenreId = 1
            };
            UpdateBookCommandValidator validationRules = new UpdateBookCommandValidator();
            var result = validationRules.Validate(command);

            result.Errors.Count.Should().Equals(0);

        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldUpdated()
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = 1;
            command.Model = new UpdateBookModel
            {
                Title = "Lord Of The Rings Return Of The King",
                PageCount = 100,
                PublishDate = DateTime.Now.Date.AddYears(-5),
                GenreId = 1
            };

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var book = _context.Books.FirstOrDefault(x => x.Title == command.Model.Title);
            book.Should().NotBeNull();
            book.PageCount.Should().Be(command.Model.PageCount);
            book.PublishDate.Should().Be(command.Model.PublishDate);
            book.GenreId.Should().Be(command.Model.GenreId);
        }


    }
}
