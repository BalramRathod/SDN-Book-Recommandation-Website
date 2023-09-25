using EHR_Multilayer.Domain.Entities;
using EHR_Multilayer.ViewModel.BookModel;
using EHR_Multilayer.ViewModel.BookSearchDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR_Multilayer.Repository.BookRepository
{   public interface IBookRepository
    {
        Task<IEnumerable<Book>> Search(string Title,string Author, string Genres);
        Task<IEnumerable<Book>> GetBooksByCriteriaAsync(string searchTerm, string sortBy, int pageNumber, int pageSize);

        Task<IEnumerable<Book>> SearchWithTitle(string Title);
        Task<IEnumerable<Book>> SearchWithAuthor(string Author);
        Task<IEnumerable<Book>> SearchWithGenres(string Genres);


        List<Book> GetAll();
        Book GetById(int id);
        Book Add(Book book);
        Book Update(Book bookDto);
        void Delete(int id);
        List<SearchBookDto> GetReferralDashboardData<T>(string filterDTO) where T : class, new();


        // 
        public Task<IEnumerable<Book>> sortByYear();
        public Task<IEnumerable<Book>> sortByYearDes();

        IEnumerable<BookWithAverageRating> GetBooksByRating(int minRating);

    }
}
