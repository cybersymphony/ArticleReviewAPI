using ArticleReviewAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleReviewAPI.DataSources.Context
{
    public class ArticleReviewDbContextBase : DbContext
    {
        public ArticleReviewDbContextBase()
        {

        }
        public ArticleReviewDbContextBase(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Review> Reviews { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>()
                .HasOne<Article>()
                .WithMany()
                .HasForeignKey(p => p.ArticleId);
                //.OnDelete(DeleteBehavior.Cascade);
        }
    }
}
