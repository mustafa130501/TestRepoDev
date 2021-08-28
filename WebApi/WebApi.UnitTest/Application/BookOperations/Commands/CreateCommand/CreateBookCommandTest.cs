using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.BookOperations.CreateBook;
using WebApi.DataAccess;
using WebApi.Entities;
using WebApi.UnitTest.TestSetup;
using Xunit;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;

namespace WebApi.UnitTest.Application.BookOperations.Commands.CreateCommand
{
    public class CreateBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookContext _context;
        private readonly IMapper _mapper;

        public CreateBookCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        /*
         ARRANGE : HAZIRLIK
         ACT     : ÇALIŞTIRMA
         ASSERT  : DOĞRULAMA
         */
        [Theory]
        [InlineData("", 0, 0)]
        [InlineData("Lord Of The Rings",0,0)]
        [InlineData("Lord Of The Rings", 0, 1)]
        [InlineData("Lord Of The Rings", 100, 0)]
        [InlineData("", 100, 1)]
        [InlineData("", 0, 1)]
        [InlineData("L", 100, 1)]
        [InlineData("Lo", 100, 1)]
        [InlineData("Lord", 0, 0)]
       //[InlineData("Lord Of The Rings", 100, 1)] // Geçerli Veri Hata Verecektir.

        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title,int pageCount,int genreId)
        {
            //arrange
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = new CreateBookModel
            {
                Title = title,
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date.AddYears(-5),
                GenreId = genreId
            };

            //act
            CreateBookCommandValidator validationRules = new CreateBookCommandValidator();
            var result = validationRules.Validate(command);

            //assert

            result.Errors.Count.Should().BeGreaterThan(0);
        }


        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = new CreateBookModel
            {
                Title = "Lord Of The Rings",
                PageCount = 100,
                PublishDate = DateTime.Now.Date,
                GenreId = 1
            };
            CreateBookCommandValidator  validationRules= new CreateBookCommandValidator();  
            var result = validationRules.Validate(command); 

            result.Errors.Count.Should().Equals(0);

        }




        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShoultNotBeReturnErrors()
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = new CreateBookModel
            {
                Title = "Lord Of The Rings",
                PageCount = 100,
                PublishDate = DateTime.Now.Date.AddYears(-5),
                GenreId = 1
            };
            CreateBookCommandValidator validationRules = new CreateBookCommandValidator();
            var result = validationRules.Validate(command);

            result.Errors.Count.Should().BeLessThan(1);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            //arrange
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = new CreateBookModel
            {
                Title = "Lord Of The Rings",
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
