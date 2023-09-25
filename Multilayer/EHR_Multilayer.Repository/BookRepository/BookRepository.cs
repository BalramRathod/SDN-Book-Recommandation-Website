using EHR_Multilayer.Domain.EHRContext;
using EHR_Multilayer.Domain.Entities;
using EHR_Multilayer.ViewModel.BookModel;
using EHR_Multilayer.ViewModel.BookSearchDto;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EHR_Multilayer.Repository.BookRepository
{
    public class BookRepository : IBookRepository
    {
        private readonly SampleContext _context;


        public BookRepository(SampleContext context)
        {
            _context = context;
        }

      

        public Book GetById(int id)
        {
            var book=   _context.Books.Find(id);
            return book;
        }

        public Book Add(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
            return book;
        }



        public void Delete(int id)
        {
            var existingBook = _context.Books.FirstOrDefault(b => b.Id == id);
            if (existingBook != null)
            {
                _context.Books.Remove(existingBook);
                _context.SaveChanges();
            }
            
        }
        
        public Book Update(Book bookDto)
        {
            _context.Books.Update(bookDto);
             _context.SaveChanges();
              return bookDto;

        }

        public List<Book> GetAll()
        {
            List<Book> book = _context.Books.ToList();
            return book;
        }



        public async Task<IEnumerable<Book>> sortByYear()
        {
            var books = _context.Books.OrderBy(b => b.publishYear).ToList();
            return books;
        }
        public async Task<IEnumerable<Book>> sortByYearDes()
        {
            var books = _context.Books.OrderByDescending(b => b.publishYear).ToList();
            return books;
        }

       

   
        public List<SearchBookDto> GetReferralDashboardData<T>(string filterDTO) where T : class, new()
      {
            SqlParameter[] parameters = {
                
                new SqlParameter("@SearchText",filterDTO),
            };
            return _context.ExecStoredProcedureForReferralDashboard("sp_SearchBooks", parameters.Length, parameters);
        }


        public IEnumerable<BookWithAverageRating> GetBooksByRating(int minRating)
        {

            var booksWithAvgRating = _context.Books
            .Join(
                    _context.Reviews,
                    book => book.Id,
                    review => review.Book_Id,
                    (book, review) => new
                    {
                        Book = book,
                        Review = review
                    }
                )
                .GroupBy(
                    b => new { b.Book.Id, b.Book.Title, b.Book.Author, b.Book.CoverImage },
                    g => g.Review.Rating
                )
                .Select(
                    g => new BookWithAverageRating
                    {
                        Id = g.Key.Id,
                        Title = g.Key.Title,
                        Author = g.Key.Author,
                        CoverImage = g.Key.CoverImage,
                        AverageRating = g.Average()
                    }
                )
                .Where(b => b.AverageRating >= minRating)
                .ToList();

            return booksWithAvgRating;
        }




























        public async Task<IEnumerable<Book>> Search(string Title, string Author, string Genres)
        {
            IQueryable<Book> query = _context.Books;
            if (!string.IsNullOrEmpty(Title))
            {
                query = query.Where(b => b.Title.Contains(Title));
            }
            if (!string.IsNullOrEmpty(Author))
            {
                query = query.Where(b => b.Author.Contains(Author));
            }
            if (!string.IsNullOrEmpty(Genres))
            {
                query = query.Where(b => b.Genres.Contains(Genres));
            }

            return await query.ToListAsync();
        }


        public async Task<IEnumerable<Book>> GetBooksByCriteriaAsync(string searchTerm, string sortBy, int pageNumber, int pageSize)
        {
            // Implement filtering and sorting logic here based on the provided criteria.
            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(b => b.Title.Contains(searchTerm) || b.Author.Contains(searchTerm));
            }

            switch (sortBy.ToLower())
            {
                case "title":
                    query = query.OrderBy(b => b.Title);
                    break;
                case "author":
                    query = query.OrderBy(b => b.Author);
                    break;
                case "publishyear":
                    query = query.OrderBy(b => b.publishYear);
                    break;
                // Add more sorting options as needed.
                default:
                    query = query.OrderBy(b => b.Id); // Default sorting by ID.
                    break;
            }

            return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<Book>> SearchWithTitle(string Title)
        {
            IQueryable<Book> query = _context.Books;
            if (!string.IsNullOrEmpty(Title))
            {
                query = query.Where(b => b.Title.Contains(Title));
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Book>> SearchWithAuthor(string Author)
        {
            IQueryable<Book> query = _context.Books;

            if (!string.IsNullOrEmpty(Author))
            {
                query = query.Where(b => b.Author.Contains(Author));
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Book>> SearchWithGenres(string Genres)
        {
            IQueryable<Book> query = _context.Books;

            if (!string.IsNullOrEmpty(Genres))
            {
                query = query.Where(b => b.Genres.Contains(Genres));
            }

            return await query.ToListAsync();
        }



    
    }
}
