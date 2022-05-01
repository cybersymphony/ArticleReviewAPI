using ArticleReviewAPI.DataSources.Context;
using ArticleReviewAPI.DataSources.Interface;
using ArticleReviewAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleReviewAPI.DataSources
{
    public class ReviewDataSource : IReviewDataSource
    {
        ArticleReviewDbContextBase ArticleReviewDbContext { get; set; }
         public ReviewDataSource(ArticleReviewDbContextBase articleReviewDbContext)
        {
            ArticleReviewDbContext = articleReviewDbContext;
        }
        public List<Review> ListReviews()
        {
            try
            {
                return ArticleReviewDbContext.Reviews.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Review AddReview(Review review)
        {
            try
            {
                ArticleReviewDbContext.Add(review);
                ArticleReviewDbContext.SaveChanges();
                return (Review)review.DeepClone();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool UpdateReview(Review review)
        {
            try
            {
                Review dbObject = GetReviewFromDB(review.Id);
                if (dbObject == null)
                    return false;
                dbObject.Overwrite(review);
                ArticleReviewDbContext.Update(dbObject);
                ArticleReviewDbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeleteReview(int reviewID)
        {
            try
            {
                var dbObject = GetReviewFromDB(reviewID);
                if (dbObject == null)
                    return false;
                ArticleReviewDbContext.Remove(dbObject);
                ArticleReviewDbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public Review GetReviewFromDB(int reviewID)
        {
            return ArticleReviewDbContext.Reviews.SingleOrDefault(x => x.Id == reviewID);
        }
    }
}
