using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataAccess;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public UpdateAuthorModel Model;
        public int AuthorId;

        private readonly IBookContext _context;
        private readonly IMapper _mapper;
        public UpdateAuthorCommand(IBookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {




            var author = _context.Authors.FirstOrDefault(x => x.Id == AuthorId);

            if (author is  null)
            {
                throw new InvalidOperationException("Author Not Found");
            }

            if (_context.Authors.Any(x=>x.Name.Trim().ToLower()==Model.Name.Trim().ToLower()))
            {
                throw new InvalidOperationException("This author is already registered");

            }


            author.Name = Model.Name == default ? author.Name : Model.Name;
            author.Surname = Model.Surname==default ? author.Surname : Model.Surname;
            author.BirthDate = Model.BirthDate.Date>DateTime.Now.Date.AddYears(-10)  ? author.BirthDate : Model.BirthDate;  

            _context.SaveChanges();
        }
    }

    public class UpdateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
