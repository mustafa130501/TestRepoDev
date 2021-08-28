using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataAccess;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {

        public CreateGenreModel Model { get; set; }

        private readonly IBookContext _context;
        private readonly IMapper _mapper;
        public CreateGenreCommand(IBookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var genre = _context.Genres.FirstOrDefault(x => x.Name == Model.Name);

            if (genre is not null)
            {
                throw new InvalidOperationException("Book Genre Already Existing");
            }

            genre = _mapper.Map<Genre>(Model);
            _context.Genres.Add(genre);
            _context.SaveChanges();
        }
    }

    public class CreateGenreModel
    {
        public string Name { get; set; }
    }
}
