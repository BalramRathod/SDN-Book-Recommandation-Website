using AutoMapper;
using EHR_Multilayer.Domain.Entities;
using EHR_Multilayer.Domain.JSONModel;
using EHR_Multilayer.Domain.StatuMessageModel;
using EHR_Multilayer.Repository.ReviewRepository;
using EHR_Multilayer.ViewModel.ReviewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EHR_Multilayer.Domain.StatuMessageModel.StatusMessage;

namespace EHR_Multilayer.Service.ReviewServices
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository repository;
        private readonly IMapper _mapper;

        public ReviewService(IReviewRepository repository, IMapper mapper)
        {
            this.repository = repository;
                 _mapper = mapper;
        }

        public JsonModel AddReview(ReviewDto reviewDto)
        {
            JsonModel response = new JsonModel();

            if (reviewDto != null)
            {
                Review Review = new Review();
                Review.User_Id = reviewDto.User_Id;
                Review.Book_Id = reviewDto.Book_Id;
                Review.review = reviewDto.review;
                Review.Rating = reviewDto.Rating;

                Review reviewData = _mapper.Map<Review>(reviewDto); ;
                var entity = repository.Add(reviewData);

                if (entity != null)
                {
                    response = new JsonModel(entity, null, StatusMessage.SuccessReview, (int)HttpStatusCodes.OK);
                }
                else
                {
                    response = new JsonModel(new object(), null, StatusMessage.NotFound, (int)HttpStatusCodes.NotFound, appError: "this is error");
                }

            }

            return response;
        }

        public List<Review> getAllReviews()
        {
            return repository.GetAll();
        }

        public JsonModel GetReviewById(int id)
        {
            JsonModel response = new JsonModel();
            if (id != 0)
            {
                var reviewEntity = repository.GetById((int)id);


                if (reviewEntity != null)
                {
                    response = new JsonModel(reviewEntity, null, StatusMessage.FetchMessage, (int)HttpStatusCodes.OK);
                }
                else
                {
                    response = new JsonModel(new object(), null, StatusMessage.NotFound, (int)HttpStatusCodes.NotFound, appError: "this is error");
                }
            }

            return response;
        }
    }
}
