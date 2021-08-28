using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.DataAccess
{
    public interface IBookContext
    {
        DbSet<Book> Books { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<Author> Authors { get; set; }
        DbSet<User> Users {  get;}
        int SaveChanges();
    }
}
