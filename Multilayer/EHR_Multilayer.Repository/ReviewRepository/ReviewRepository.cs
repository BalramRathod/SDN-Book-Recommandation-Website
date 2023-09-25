using EHR_Multilayer.Domain.EHRContext;
using EHR_Multilayer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR_Multilayer.Repository.ReviewRepository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly SampleContext _context;

        public ReviewRepository(SampleContext context)
        {
            _context = context;
        }

        public Review Add(Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
            return review;
        }

        public List<Review> GetAll()
        {
            var res = _context.Reviews.ToList();
            return res;
        }

        List<Review> IReviewRepository.GetById(int id)
        {
            var res = _context.Reviews.Where(x => x.Book_Id == id).ToList();
            return res;
        }
    }
}
