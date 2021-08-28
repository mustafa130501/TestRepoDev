using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DataAccess;
using WebApi.Entities;

namespace WebApi.UnitTest.TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookContext context)
        {
            context.Authors.AddRange(
                       new Author { Name = "Joanne Kathleen", Surname = "Rowling", BirthDate = new DateTime(1965, 7, 31) },
                       new Author { Name = "John Ronald Reuel", Surname = "Tolkien", BirthDate = new DateTime(1892, 1, 3) },
                       new Author { Name = "Lev Nikolayeviç", Surname = "Tolstoy", BirthDate = new DateTime(1828, 9, 9) },
                       new Author { Name = "George", Surname = "Orwell", BirthDate = new DateTime(1903, 6, 25) },
                       new Author { Name = "Stephan", Surname = "King", BirthDate = new DateTime(1947, 9, 21) }
                   );
        }
    }
}
