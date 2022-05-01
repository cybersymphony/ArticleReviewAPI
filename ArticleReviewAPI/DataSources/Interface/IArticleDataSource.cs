using ArticleReviewAPI.Models;
using System.Collections.Generic;

namespace ArticleReviewAPI.DataSources.Interface
{
    public interface IArticleDataSource
    {
        List<Article> ListArticles();
        Article AddArticle(Article article);
        bool UpdateArticle(Article article);
        bool DeleteArticle(int articleID);
        Article GetArticleFromDB(int articleID);
    }
}