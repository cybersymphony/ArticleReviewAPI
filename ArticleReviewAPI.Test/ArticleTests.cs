using NUnit.Framework;
using System.Linq;

namespace ArticleReviewAPI.Test
{
    public class ArticleTests
    {
        TestHelper testHelper;
        [SetUp]
        public void Setup()
        {
            testHelper = new TestHelper();
        }

        [Test]
        public void InsertArticle()
        {
            testHelper.ArticleController.Post(new Models.Article()
            {
                ArticleContent = "Article Content",
                PublishDate = new System.DateTime(2012, 1, 1, 1, 1, 1),
                StarCount = 3,
                Author = "Sam Adams",
                Title = "Title 1"
            });
            var articleList = testHelper.ArticleController.Get().ToList();

            Assert.Greater(articleList.Count, 0);
        }
    }
}