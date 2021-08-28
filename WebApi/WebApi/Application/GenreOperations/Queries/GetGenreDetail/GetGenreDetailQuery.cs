using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataAccess;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        private readonly IBookContext _context;
        private readonly IMapper _mapper;
        public int GenreId { get; set; }
        public GetGenreDetailQuery(IBookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GenreDetailViewModel Handle()
        {
            Genre genre = _context.Genres.FirstOrDefault(x => x.Id == GenreId && x.IsActive);

            if (genre is null)
            {
                throw new InvalidOperationException("Book Genre Not Found");
            }
            GenreDetailViewModel view = _mapper.Map<GenreDetailViewModel>(genre);
            return view;
        }
    }

    public class GenreDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
