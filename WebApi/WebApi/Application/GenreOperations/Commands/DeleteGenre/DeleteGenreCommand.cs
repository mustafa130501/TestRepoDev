using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataAccess;

namespace WebApi.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int  GenreId { get; set; }
        private readonly IBookContext _context;
        public DeleteGenreCommand(IBookContext context, IMapper mapper)
        {
            _context = context;
        }

        public void Handle()
        {

            var genre = _context.Genres.FirstOrDefault(x => x.Id == GenreId);

            if (genre is  null)
            {
                throw new InvalidOperationException("Book Genre Not Found");
            }
            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }
    }
}
