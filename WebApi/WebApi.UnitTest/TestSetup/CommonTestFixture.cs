using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Common;
using WebApi.DataAccess;

namespace WebApi.UnitTest.TestSetup
{
    public class CommonTestFixture
    {
        public BookContext Context {  get; set; }
        public IMapper Mapper {  get; set; }

        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<BookContext>().UseInMemoryDatabase("BookStoreTestDB").Options;
            Context = new BookContext(options);
            Context.Database.EnsureCreated();
            Context.AddBooks();
            Context.AddGenres();
            Context.AddAuthors();
            Context.SaveChanges();

            Mapper = new MapperConfiguration(config => { config.AddProfile<MappingProfile>(); }).CreateMapper();
        }
    }
}
