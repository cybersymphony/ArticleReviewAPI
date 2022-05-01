using ArticleReviewAPI.Controllers;
using ArticleReviewAPI.DataSources;
using ArticleReviewAPI.DataSources.Context;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleReviewAPI.Test
{
    class TestHelper : IDisposable
    {
        private SqliteConnection _connection;
        private DbContextOptions _options;
        private ArticleReviewDbContextBase mockDB;
        private ArticleDataSource articleDataSource;
        private ReviewDataSource reviewDataSource;
        public ArticleController ArticleController
        {
            get
            {
                return new ArticleController(articleDataSource);
            }
        }
        public ReviewController ReviewController
        {
            get
            {
                return new ReviewController(reviewDataSource, articleDataSource);
            }
        }
        public TestHelper()
        {

            _connection = new SqliteConnection("datasource=:memory:");
            _connection.Open();
            _options = new DbContextOptionsBuilder()
                .UseSqlite(_connection)
                .Options;
            mockDB = new ArticleReviewDbContextBase(_options);
            mockDB.Database.EnsureCreated();

            articleDataSource = new ArticleDataSource(mockDB);
            reviewDataSource = new ReviewDataSource(mockDB);

        }
        public void Dispose()
        {
            _connection.Close();
        }

    }
}
