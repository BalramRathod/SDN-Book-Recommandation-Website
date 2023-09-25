using AutoMapper;
using EHR_Multilayer.Domain.Entities;
using EHR_Multilayer.Domain.JSONModel;
using EHR_Multilayer.Domain.StatuMessageModel;
using EHR_Multilayer.Repository.BookRepository;
using EHR_Multilayer.ViewModel.BookModel;
using EHR_Multilayer.ViewModel.BookSearchDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static EHR_Multilayer.Domain.StatuMessageModel.StatusMessage;

namespace EHR_Multilayer.Service.BookServices
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public JsonModel AddBook(BookDto bookDto)
        {
            JsonModel response = new JsonModel();

            Book book = new Book();
            book.Author = bookDto.Author;
            book.Title = bookDto.Title;
            book.CoverImage = bookDto.CoverImage;
            book.Genres = bookDto.Genres;
            book.publishYear = bookDto.publishYear;

            Book bookData = _mapper.Map<Book>(book);
            var bookEntity = _bookRepository.Add(bookData);
            if (bookEntity != null)
            {
                response = new JsonModel(bookEntity, null, StatusMessage.Success, (int)HttpStatusCodes.OK);
            }
            else
            {
                response = new JsonModel(new object(), null, StatusMessage.NotFound, (int)HttpStatusCodes.NotFound, appError: "This is app error");
            }

            return response;
        }

        public void DeleteBook(int id)
        {
           _bookRepository.Delete(id);
        }

   
        public JsonModel GetBookById(int id)
        {
            JsonModel response = new JsonModel();

            if (id != 0)
            {
                var bookEntity = _bookRepository.GetById((int)id);


                if (bookEntity != null)
                {
                    response = new JsonModel(bookEntity, null, StatusMessage.FetchMessage, (int)HttpStatusCodes.OK);
                }
                else
                {
                    response = new JsonModel(new object(), null, StatusMessage.NotFound, (int)HttpStatusCodes.NotFound, appError: "this is error");
                }
            }

            return response;
        }


        public JsonModel GetAllBooks()
        {
            JsonModel response = new JsonModel();
            List<Book> entity = null;

            entity = _bookRepository.GetAll().ToList();

            if (entity != null)
            {
                List<Book> soapsDto = _mapper.Map<List<Book>>(entity);


                response = new JsonModel(entity, null, StatusMessage.FetchMessage, (int)HttpStatusCodes.OK);
            }
            else
            {
                response = new JsonModel(new object(), null, StatusMessage.NotFound, (int)HttpStatusCodes.NotFound, appError: "this is error");
            }

            // Get all countries in decrypted form from DB

            return response;
        }

        public JsonModel UpdateBook(BookDto bookDto, int id)
        {
            JsonModel response = new JsonModel();
            var book = _bookRepository.GetById(id);
            if(book != null)
            {
                book.Author = bookDto.Author;
                book.Title = bookDto.Title;
                book.CoverImage = bookDto.CoverImage;
                book.Genres = bookDto.Genres;
                book.publishYear = bookDto.publishYear;

                Book updatBook = _mapper.Map<Book>(book);

                var entiy1 =_bookRepository.Update(updatBook);
                if (entiy1 != null)
                {
                    response = new JsonModel(entiy1, null, StatusMessage.UpdatedSuccessfully, (int)HttpStatusCodes.OK);
                }
                else
                {
                    response = new JsonModel(new object(), null, StatusMessage.NotFound, (int)HttpStatusCodes.NotFound, appError: "this is error");
                }
            }

            return response;
        }

        public JsonModel GetSearch(BookSearchDto searcDto)
        {
            JsonModel response = new JsonModel();

            return response;
        }

       
        public JsonModel GetBookProcedureData(string filterDTO)
        {
            JsonModel response = new JsonModel();
            List<SearchBookDto> data = _bookRepository.GetReferralDashboardData<List<SearchBookDto>>(filterDTO);
            response.data = data;
            response.Message = StatusMessage.FetchMessage;
            response.StatusCode = (int)HttpStatusCode.OK;
            return response;

        }
    }


}
