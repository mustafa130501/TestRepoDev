using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.DataAccess
{
    public class DataGenerator
    {
        //INMEMORY Database 

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookContext(serviceProvider.GetRequiredService<DbContextOptions<BookContext>>()))
            {
                if (!context.Books.Any())
                {
                    context.Books.AddRange(
                      new Book()
                      {

                          Title = "Harry Potter",
                          GenreId = 2,
                          AuthorId = 1,
                          PageCount = 500,
                          PublishDate = new DateTime(2001, 6, 12)
                      },
                      new Book()
                      {

                          Title = "Hobbit",
                          GenreId = 2,
                          AuthorId=2,
                          PageCount = 1000,
                          PublishDate = new DateTime(2000, 6, 21)
                      },
                      new Book()
                      {
                          Title = "Alice is Wonderland ",
                          GenreId = 1,
                          AuthorId=3,
                          PageCount = 200,
                          PublishDate = new DateTime(2008, 11, 10)
                      });
                }

                

                if (!context.Genres.Any())
                {
                    context.Genres.AddRange(
                      new Genre { Name = "Personal Growth" },
                      new Genre { Name = "Science Fiction" },
                      new Genre { Name = "Kid" }
                    );

                }

                if (!context.Authors.Any())
                {
                    context.Authors.AddRange(
                        new Author { Name="Joanne Kathleen",Surname="Rowling",BirthDate=new DateTime(1965,7,31)},
                        new Author { Name= "John Ronald Reuel",Surname="Tolkien",BirthDate=new DateTime(1892,1,3)},
                        new Author { Name= "Lev Nikolayeviç",Surname="Tolstoy",BirthDate=new DateTime(1828,9,9) },
                        new Author { Name = "George", Surname = "Orwell", BirthDate = new DateTime(1903, 6, 25) },
                        new Author { Name = "Stephan", Surname = "King", BirthDate = new DateTime(1947, 9, 21) }
                    );
                }

                context.SaveChanges();

            }
        }
    }
}
