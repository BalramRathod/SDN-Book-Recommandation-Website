using EHR_Multilayer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR_Multilayer.Repository.ReviewRepository
{
    public interface IReviewRepository
    {

       Review Add(Review review);
        List<Review> GetAll();
        List<Review> GetById(int id);
    }
}
