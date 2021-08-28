using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataAccess;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        public UpdateBookModel Model { get; set; }
        public int BookId {  get; set; }
        private readonly IBookContext _context;

        public UpdateBookCommand(IBookContext bookContext)
        {
            _context = bookContext;
        }
        public void Handle()
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == BookId);
            if (book is null)
            {
                throw new InvalidOperationException("No Book Found to Update");
            }

            
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
            book.Title = Model.Title != default ? Model.Title : book.Title;
            book.PublishDate = Model.PublishDate != default ? Model.PublishDate : book.PublishDate;
            _context.SaveChanges();
        }

    }

    public class UpdateBookModel
    {
        
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
