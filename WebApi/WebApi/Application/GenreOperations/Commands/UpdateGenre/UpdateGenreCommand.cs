using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataAccess;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public UpdateGenreModel Model { get; set; }
        public int GenreId { get; set; }

        private readonly IBookContext _context;
        private readonly IMapper _mapper;
        public UpdateGenreCommand(IBookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var genre = _context.Genres.FirstOrDefault(x => x.Id == GenreId);

            if (genre is  null)
            {
                throw new InvalidOperationException("Book Genre Not Found");
            }

            if (_context.Genres.Any(x=>x.Name.ToLower()==Model.Name.ToLower() && x.Id!=GenreId))
            {
                throw new InvalidOperationException("A book genre with the same name already exists");
            }

            genre.Name = Model.Name.Trim() == default ? genre.Name : Model.Name;
            genre.IsActive = Model.IsActive;
            _context.SaveChanges();
        }
    }

    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}

