using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Common;
using WebApi.DataAccess;
using WebApi.Entities;

namespace WebApi.BookOperations.BookDetail
{
    public class GetBookDetailQuery
    {
        private readonly IBookContext _context;
        private readonly IMapper _mapper;
        public int BookId { get; set; }
        public GetBookDetailQuery(IBookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle()
        {
            Book book = _context.Books.Include(x=>x.Genre).Include(x=>x.Author).FirstOrDefault(x => x.Id == BookId);

            if (book == null)
            {
                throw new InvalidOperationException("Book Not Found");
            }
            BookDetailViewModel view = _mapper.Map<BookDetailViewModel>(book);
            return view;
        }
        
    }
    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Author { get; set; }
    }
}
