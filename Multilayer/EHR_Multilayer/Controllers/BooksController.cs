using EHR_Multilayer.Domain.EHRContext;
using EHR_Multilayer.Domain.Entities;
using EHR_Multilayer.Repository.BookRepository;
using EHR_Multilayer.Service.BookServices;
using EHR_Multilayer.ViewModel.BookModel;
using EHR_Multilayer.ViewModel.BookSearchDto;
using EHR_Multilayer.ViewModel.UserModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace EHR_Multilayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IBookRepository repo;
        private readonly SampleContext _context;

        public BooksController(IBookService bookService, SampleContext sampleContext, IBookRepository bookRepository)
        {
            _bookService = bookService;
            _context = sampleContext;
            repo = bookRepository;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            return Ok(_bookService.GetAllBooks());
          
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
            {
   
            return Ok(_bookService.GetBookById(id));
        }

        [HttpPost("AddBook")]
        public IActionResult AddBook(BookDto bookDto)
        {
            return Ok(_bookService.AddBook(bookDto));
          
        }

    


        [HttpPut("updateBook/{id}")]
        public IActionResult UpdateBook(BookDto bookDto, int id)
        {
            return Ok(_bookService.UpdateBook(bookDto,id));
        }
     

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            _bookService.DeleteBook(id);
            return Ok("Book has been deleted"); 


        }


        [HttpGet("Search")]
        public IActionResult SearchBooksAsyncTESt(string searchText)
        {

            var searchResults = _bookService.GetBookProcedureData(searchText);

            return Ok(searchResults);
        }




        [HttpGet("SortYearByAscendingOrder")]
        public async Task<IActionResult> SortYearByAscendingOrder()
        {
            try
            {
                var SoretedBooks = await repo.sortByYear();
                
                    return Ok(SoretedBooks);
                
                return Ok(new { message = "not found" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("SortYearByDescendingOrder")]
        public async Task<IActionResult> SortYearByDescendingOrder()
        {
            try
            {
                var SoretedBooksByDescendingOrder = await repo.sortByYearDes();
                if (SoretedBooksByDescendingOrder.Any())
                {
                    return Ok(SoretedBooksByDescendingOrder);
                }
                return Ok(new { message = "not found" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("filter")]
        public IActionResult FilterBooksByRating(int minRating)
        {
            var booksWithAvgRating = repo.GetBooksByRating(minRating);
            return Ok(booksWithAvgRating);
        }





    }
}
