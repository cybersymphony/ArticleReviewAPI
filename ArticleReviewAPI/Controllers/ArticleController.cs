using ArticleReviewAPI.DataSources;
using ArticleReviewAPI.DataSources.Interface;
using ArticleReviewAPI.Models;
using ArticleReviewAPI.Auth;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleReviewAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class ArticleController : ODataController
    {
        public IArticleDataSource ArticleDataSource { get; set; }
        public IReviewDataSource ReviewDataSource { get; set; }
        public ArticleController(IReviewDataSource reviewDataSource, IArticleDataSource articleDataSource)
        {
            ReviewDataSource = reviewDataSource;
            ArticleDataSource = articleDataSource;
        }
        [HttpGet]
        [EnableQuery]
        [ODataRoute("Articles")]
        public IEnumerable<Article> Get()
        {
            return ArticleDataSource.ListArticles();
        }
        [HttpPost]
        public IActionResult Post([FromBody] Article article)
        {
            if (!article.ValidateModel())
                return BadRequest(false);

            var result = ArticleDataSource.AddArticle(article);
            if (result != null)
                return Ok(result);
            return BadRequest(false);
        }
        [HttpPut]
        public IActionResult Put([FromBody] Article article)
        {
            if (!article.ValidateModel())
                return BadRequest(false);


            var result = ArticleDataSource.UpdateArticle(article);
            if (result)
                return Ok(result);
            return BadRequest(false);
        }
        [HttpDelete]
        public IActionResult Delete(int articleID)
        {
            var dbOject = ArticleDataSource.GetArticleFromDB(articleID);
            if (dbOject != null)
            {
                bool isDeletable = !ReviewDataSource.ListReviews().ToList().Any(x => x.ArticleId == articleID);
                if (!isDeletable)
                    return BadRequest(false);
            }

            var result = ArticleDataSource.DeleteArticle(articleID);
            if (result)
                return Ok(result);
            return BadRequest(false);
        }
    }
}
