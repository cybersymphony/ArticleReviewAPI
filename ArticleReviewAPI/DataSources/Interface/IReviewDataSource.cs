using ArticleReviewAPI.Models;
using System.Collections.Generic;

namespace ArticleReviewAPI.DataSources.Interface
{
    public interface IReviewDataSource
    {
        List<Review> ListReviews();
        Review AddReview(Review review);
        bool UpdateReview(Review review);
        bool DeleteReview(int reviewID);
        Review GetReviewFromDB(int reviewID);
    }
}