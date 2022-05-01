using ArticleReviewAPI.Models;
using ArticleReviewAPI.DataSources;
using ArticleReviewAPI.DataSources.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;

namespace ArticleReviewAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class ReviewController : ODataController
    {
        public IArticleDataSource ArticleDataSource { get; set; }
        public IReviewDataSource ReviewDataSource { get; set; }
        public ReviewController(IReviewDataSource reviewDataSource, IArticleDataSource articleDataSource)
        {
            ReviewDataSource = reviewDataSource;
            ArticleDataSource = articleDataSource;
        }
        [HttpGet]
        [EnableQuery]
        [ODataRoute("Reviews")]
        public IEnumerable<Review> Get()
        {
            return ReviewDataSource.ListReviews();
        }
        [HttpPost]
        public IActionResult Post([FromBody] Review review)
        {
            if (!review.ValidateModel())
                return BadRequest(false);

            var relatedArticle = ArticleDataSource.GetArticleFromDB(review.ArticleId);
            if (relatedArticle == null)
                return BadRequest(false);


            var result = ReviewDataSource.AddReview(review);
            if (result != null)
                return Ok(result);
            return BadRequest(false);
        }
        [HttpPut]
        public IActionResult Put([FromBody] Review review)
        {
            if (!review.ValidateModel())
                return BadRequest(false);
            var relatedArticle = ArticleDataSource.GetArticleFromDB(review.ArticleId);
            if (relatedArticle == null)
                return BadRequest(false);

            var result = ReviewDataSource.UpdateReview(review);
            if (result)
                return Ok(result);
            return BadRequest(false);
        }
        [HttpDelete]
        public IActionResult Delete(int reviewID)
        {
            var result = ReviewDataSource.DeleteReview(reviewID);
            if (result)
                return Ok(result);
            return BadRequest(false);
        }
    }
}
