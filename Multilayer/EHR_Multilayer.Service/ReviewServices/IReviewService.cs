using EHR_Multilayer.Domain.Entities;
using EHR_Multilayer.Domain.JSONModel;
using EHR_Multilayer.ViewModel.ReviewModel;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Review = EHR_Multilayer.Domain.Entities.Review;

namespace EHR_Multilayer.Service.ReviewServices
{
    public interface IReviewService
    {
        JsonModel AddReview (ReviewDto reviewDto);
        List<Review> getAllReviews();
        public JsonModel GetReviewById(int id);
    }
}
