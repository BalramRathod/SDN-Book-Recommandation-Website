using EHR_Multilayer.Domain.EHRContext;
using EHR_Multilayer.Domain.Entities;
using EHR_Multilayer.Service.ReviewServices;
using EHR_Multilayer.ViewModel.ReviewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe.Treasury;

namespace EHR_Multilayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService service;

        private readonly SampleContext _context;
     
        public ReviewsController(IReviewService service, SampleContext context)
        {
            this.service = service;
            _context = context;
        }

        [HttpPost("Add")]
        public IActionResult addReview(ReviewDto reviewDto)
        {
            return Ok(service.AddReview(reviewDto));
        }
        [HttpGet("GetAll")]
        public IActionResult getAllReviews()
        {
            return Ok(service.getAllReviews());
        }


        // getting reviews based on bookId
        [HttpGet("{id}")]
        public IActionResult getReviewsOnBookById(int id)
        {
            return Ok(service.GetReviewById(id));
        }



        

       

    }
}
