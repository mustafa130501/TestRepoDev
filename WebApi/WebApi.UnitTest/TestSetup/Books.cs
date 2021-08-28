using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DataAccess;
using WebApi.Entities;

namespace WebApi.UnitTest.TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookContext context)
        {
            context.Books.AddRange(
                   new Book()
                   {

                       Title = "Harry Potter Sorcerer's Stone",
                       GenreId = 2,
                       AuthorId = 1,
                       PageCount = 860,
                       PublishDate = new DateTime(2001, 6, 12)
                   },
                   new Book()
                   {

                       Title = "Hobbit",
                       GenreId = 2,
                       AuthorId = 2,
                       PageCount = 1300,
                       PublishDate = new DateTime(2010, 5, 23)
                   },
                   new Book()
                   {
                       Title = "İnsan Ne İle Yaşar ? ",
                       GenreId = 1,
                       AuthorId = 3,
                       PageCount = 120,
                       PublishDate = new DateTime(2002, 12, 21)
                   });
        }
    }
}
