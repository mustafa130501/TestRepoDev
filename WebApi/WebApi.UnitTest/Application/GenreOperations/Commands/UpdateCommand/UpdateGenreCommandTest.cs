using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DataAccess;
using WebApi.UnitTest.TestSetup;
using Xunit;

namespace WebApi.UnitTest.Application.GenreOperations.Commands.UpdateCommand
{
    public class UpdateGenreCommandTest  : IClassFixture<CommonTestFixture>
    {
        private readonly BookContext _context;
        private readonly IMapper _mapper;

        public UpdateGenreCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }


        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("as")]
        [InlineData("asd")]
        //[InlineData(1, "Lord", 100, 1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name)
        {

            //arrange
            UpdateGenreCommand command = new UpdateGenreCommand(_context, _mapper);
            command.Model = new UpdateGenreModel
            {
                Name = name
            };


            //act
            UpdateGenreCommandValidator validationRules = new UpdateGenreCommandValidator();
            var result = validationRules.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context,_mapper);
            command.GenreId = 1;
            command.Model = new UpdateGenreModel
            {
               Name="NewGenre"
            };
            UpdateGenreCommandValidator validationRules = new UpdateGenreCommandValidator();
            var result = validationRules.Validate(command);

            result.Errors.Count.Should().Equals(0);

        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldUpdated()
        {
            //arrange
            UpdateGenreCommand command = new UpdateGenreCommand(_context,_mapper);
            command.GenreId = 1;
            command.Model = new UpdateGenreModel
            {
               Name="NewGenre"
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
