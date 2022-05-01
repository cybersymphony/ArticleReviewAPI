using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticleReviewAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ArticleReviewAPI.DataSources.Context
{
    public class ArticleReviewDbContext : ArticleReviewDbContextBase
    {
        public ArticleReviewDbContext() : base()
        {

        }
        public ArticleReviewDbContext(DbContextOptions<ArticleReviewDbContextBase> options) : base(options)
        {
        }
    }
}
