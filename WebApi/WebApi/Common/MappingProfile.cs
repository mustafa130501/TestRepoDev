using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.BookOperations.BookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.Entities;
using static WebApi.Application.UserOperations.Commands.CreateUser.CreateUserCommand;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
       
        public MappingProfile()
        {
            
            CreateMap<CreateBookModel, Book>();
            
            CreateMap<Book, BookDetailViewModel>().ForMember(
                
                dest => dest.Genre,
                opt => opt.MapFrom(src => src.Genre.Name)).ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Name + " " + src.Author.Surname));
          
            CreateMap<Book, BooksViewModel>().ForMember(
                dest => dest.Genre,
                opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.Author,
                opt => opt.MapFrom(src => src.Author.Name + " " + src.Author.Surname)); 

            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
            CreateMap<CreateGenreModel, Genre>();
            CreateMap<UpdateGenreModel, Genre>();

            CreateMap<Author, AuthorDetailViewModel>();
            CreateMap<Author, AuthorsViewModel>();
            CreateMap<CreateAuthorModel, Author>(); 

            CreateMap<CreateUserModel, User>();

        }
    }
}
