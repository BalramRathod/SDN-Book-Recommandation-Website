using EHR_Multilayer.Domain.Entities;
using EHR_Multilayer.Domain.JSONModel;
using EHR_Multilayer.ViewModel.BookModel;
using EHR_Multilayer.ViewModel.BookSearchDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR_Multilayer.Service.BookServices
{
    public interface IBookService
    {
        JsonModel GetAllBooks();
        JsonModel GetBookById(int id);
        JsonModel AddBook(BookDto bookDto);
        JsonModel UpdateBook(BookDto bookDto, int id);
        void DeleteBook(int id);
        JsonModel GetSearch(BookSearchDto searcDto);
        JsonModel GetBookProcedureData(string filterDTO);
    }
}
