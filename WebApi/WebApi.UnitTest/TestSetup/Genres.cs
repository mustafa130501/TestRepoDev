using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DataAccess;
using WebApi.Entities;

namespace WebApi.UnitTest.TestSetup
{
    public static class Genres
    {
        public static void AddGenres(this BookContext context)
        {
            context.Genres.AddRange(
                     new Genre { Name = "Personal Growth" },
                     new Genre { Name = "Science Fiction" },
                     new Genre { Name = "Romance" }
                   );
        }
    }
}
