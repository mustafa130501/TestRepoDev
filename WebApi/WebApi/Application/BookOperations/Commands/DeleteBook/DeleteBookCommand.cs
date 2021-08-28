using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataAccess;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly IBookContext _context;
        public int BookId {  get; set; }
        public DeleteBookCommand(IBookContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == BookId);
            if (book is null)
            {
                throw new InvalidOperationException("No Book Found to be Deleted");
            }
            _context.Books.Remove(book);
            _context.SaveChanges();
        }
    }
}
