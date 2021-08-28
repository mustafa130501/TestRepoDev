using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Common;
using WebApi.DataAccess;
using WebApi.Entities;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly IBookContext _context;
        private readonly IMapper _mapper;
        public GetBooksQuery(IBookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList =  _context.Books.Include(x=>x.Genre).Include(x=>x.Author).OrderBy(x => x.Id).ToList();
            List<BooksViewModel> booksViewModel= _mapper.Map<List<BooksViewModel>>(bookList);
            return booksViewModel;
        }

    }

    public class BooksViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string  Genre { get; set; }
        public string Author { get; set; }

    }
}
