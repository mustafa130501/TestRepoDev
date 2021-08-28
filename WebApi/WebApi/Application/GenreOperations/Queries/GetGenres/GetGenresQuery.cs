using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataAccess;

namespace WebApi.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
        private readonly IBookContext _context;
        private readonly IMapper _mapper;
        public GetGenresQuery(IBookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GenresViewModel> Handle()
        {
            var genresList = _context.Genres.Where(x=>x.IsActive).OrderBy(x => x.Id).ToList();
            List<GenresViewModel> genresViewModels = _mapper.Map<List<GenresViewModel>>(genresList);
            return genresViewModels;
        }

    }

    public class GenresViewModel
    {
        public int Id { get; set; }
        public string Name {  get; set; }
    }
}
