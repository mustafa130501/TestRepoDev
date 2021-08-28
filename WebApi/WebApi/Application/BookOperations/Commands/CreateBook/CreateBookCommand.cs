using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataAccess;
using WebApi.Entities;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBookCommand
    {

        public CreateBookModel Model { get; set; }

        private readonly IBookContext _context;
        private readonly IMapper _mapper;
        public CreateBookCommand(IBookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var book = _context.Books.FirstOrDefault(x => x.Title == Model.Title);

            if (book is not null)
            {
                throw new InvalidOperationException("The book is already registered.");
            }


            

            book = _mapper.Map<Book>(Model);               //new Book();

            //book.Title = Model.Title;
            //book.PublishDate = Model.PublishDate;
            //book.PageCount = Model.PageCount;
            //book.GenreId = Model.GenreId;



            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public class CreateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }

        }


    }
}
