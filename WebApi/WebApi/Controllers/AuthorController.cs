using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.DataAccess;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]s")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IBookContext _context;
        private readonly IMapper _mapper;

        public AuthorController(IBookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetAuthors()
        {
            GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);
            return Ok(query.Handle());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorId = id;
            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            validator.ValidateAndThrow(query);
            return Ok(query.Handle());
        }

        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorModel newAuthor)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = newAuthor;
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel updatedAuthor)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context, _mapper);
            command.AuthorId = id;
            command.Model = updatedAuthor;
            UpdateAuthorCommandValidator validationRules = new UpdateAuthorCommandValidator();
            validationRules.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = id;
            DeleteAuthorCommandValidator validationRules = new DeleteAuthorCommandValidator();
            validationRules.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
    }
}
