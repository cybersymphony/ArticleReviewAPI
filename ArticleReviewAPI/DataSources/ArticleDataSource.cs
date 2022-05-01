using ArticleReviewAPI.DataSources.Context;
using ArticleReviewAPI.DataSources.Interface;
using ArticleReviewAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleReviewAPI.DataSources
{
    public class ArticleDataSource: IArticleDataSource
    {
        ArticleReviewDbContextBase ArticleReviewDbContext { get; set; }
        public ArticleDataSource(ArticleReviewDbContextBase articleReviewDbContext)
        {
            ArticleReviewDbContext = articleReviewDbContext;
        }
        public List<Article> ListArticles()
        {
            try
            {
                return ArticleReviewDbContext.Articles.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Article AddArticle(Article article)
        {
            try
            {
                ArticleReviewDbContext.Add(article);
                ArticleReviewDbContext.SaveChanges();
                return (Article)article.DeepClone();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool UpdateArticle(Article article)
        {
            try
            {
                Article dbObject = GetArticleFromDB(article.Id);
                if (dbObject == null)
                    return false;
                dbObject.Overwrite(article);
                ArticleReviewDbContext.Update(dbObject);
                ArticleReviewDbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeleteArticle(int articleID)
        {
            try
            {
                var dbObject = GetArticleFromDB(articleID);
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
        public Article GetArticleFromDB(int articleID)
        {
            return ArticleReviewDbContext.Articles.SingleOrDefault(x => x.Id == articleID);
        }
    }
}
