using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.DataAccess;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]s")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IBookContext _context;
        private readonly IMapper _mapper;


        public GenreController(IBookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetGenres()
        {
            GetGenresQuery query = new GetGenresQuery(_context,_mapper);
            return Ok(query.Handle());
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId = id;
            GenreDetailQueryValidator validator = new GenreDetailQueryValidator();
            validator.ValidateAndThrow(query);
            return Ok(query.Handle());
        }



        [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreModel newGenre)
        {
            CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);
            command.Model = newGenre;
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();

        }


        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreModel updatedBook)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context,_mapper);
            command.GenreId = id;
            command.Model = updatedBook;
            UpdateGenreCommandValidator validationRules = new UpdateGenreCommandValidator();
            validationRules.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }



        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(int id)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context,_mapper);
            command.GenreId = id;
            DeleteGenreCommandValidator validationRules = new DeleteGenreCommandValidator();
            validationRules.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
    }
}
