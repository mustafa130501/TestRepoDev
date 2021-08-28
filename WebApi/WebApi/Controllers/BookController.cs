using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.BookOperations.BookDetail;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DataAccess;
using WebApi.Entities;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]s")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookContext _context;
        private readonly IMapper _mapper;
        public BookController(IBookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            return Ok(query.Handle());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = id;
            return Ok(query.Handle());

        }


        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = newBook;
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();

        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);

            command.BookId = id;
            command.Model = updatedBook;
            UpdateBookCommandValidator validationRules = new UpdateBookCommandValidator();
            validationRules.ValidateAndThrow(command);
            command.Handle();
            return Ok();


        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {

            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = id;

            DeleteBookCommandValidator validationRules = new DeleteBookCommandValidator();
            validationRules.ValidateAndThrow(command);

            command.Handle();
            return Ok();

        }
    }
}


