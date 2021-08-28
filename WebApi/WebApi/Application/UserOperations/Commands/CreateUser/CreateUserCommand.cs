using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataAccess;
using WebApi.Entities;

namespace WebApi.Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommand
    {
        public CreateUserModel Model { get; set; }

        private readonly IBookContext _context;
        private readonly IMapper _mapper;
        public CreateUserCommand(IBookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var user = _context.Users.FirstOrDefault(x => x.UserName == Model.UserName);

            if (user is not null)
            {
                throw new InvalidOperationException("User Already Existing");
            }

           

            user = _mapper.Map<User>(Model);
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public class CreateUserModel
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public string RealName { get; set; }
        }
    }
}
