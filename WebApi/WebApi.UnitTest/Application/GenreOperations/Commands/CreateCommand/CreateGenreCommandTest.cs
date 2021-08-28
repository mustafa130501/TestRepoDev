using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DataAccess;
using WebApi.UnitTest.TestSetup;
using Xunit;

namespace WebApi.UnitTest.Application.GenreOperations.Commands.CreateCommand
{
    public class CreateGenreCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookContext _context;
        private readonly IMapper _mapper;

        public CreateGenreCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }


        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("as")]
        [InlineData("asd")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name)
        {
            //arrange
            CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);
            command.Model = new CreateGenreModel
            {
                Name = name
            };

            //act
            CreateGenreCommandValidator validationRules = new CreateGenreCommandValidator();
            var result = validationRules.Validate(command);

            //assert

            result.Errors.Count.Should().BeGreaterThan(0);
        }


        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShoultNotBeReturnErrors()
        {
            CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);
            command.Model = new CreateGenreModel
            {
                Name="Science"
            };
            CreateGenreCommandValidator validationRules = new CreateGenreCommandValidator();
            var result = validationRules.Validate(command);

            result.Errors.Count.Should().Equals(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            //arrange
            CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);
            command.Model = new CreateGenreModel
            {
               Name="Science"
            };

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var genre = _context.Genres.FirstOrDefault(x => x.Name == command.Model.Name);
            genre.Should().NotBeNull();
            genre.Name.Should().Be(command.Model.Name);
        }
    }
}
