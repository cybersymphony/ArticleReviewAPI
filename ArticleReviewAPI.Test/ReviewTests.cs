using ArticleReviewAPI.Controllers;
using ArticleReviewAPI.DataSources;
using ArticleReviewAPI.DataSources.Context;
using ArticleReviewAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;

namespace ArticleReviewAPI.Test
{
    public class ReviewTests 
    {
        TestHelper testHelper;
        [SetUp]
        public void Setup()
        {
            testHelper = new TestHelper();
        }
        [Test]
        public void InsertReview()
        {
            string testReviewContentData = "Very good article";


            testHelper.ArticleController.Post(new Models.Article()
            {
                ArticleContent = "Article Content",
                PublishDate = new System.DateTime(2012, 1, 1, 1, 1, 1),
                StarCount = 3,
                Author = "Sam Adams",
                Title = "Title 1"
            });
            var articleList = testHelper.ArticleController.Get().ToList();
            testHelper.ReviewController.Post(new Models.Review()
            {
                 ArticleId = 1,
                 ReviewContent = testReviewContentData,
                 Reviewer = "John Doe"
            });

            var reviewList = testHelper.ReviewController.Get().ToList();

            Assert.Greater(articleList.Count, 0, "Because we added a record, had to be 1 which is greater than 0.");
            Assert.AreEqual(1, reviewList.Count);
            Assert.AreEqual(testReviewContentData, reviewList.First().ReviewContent);
            
        }
        [Test]
        public void InsertReviewBeforeArticle()
        {
            string testReviewContentData = "Very good article";
            testHelper.ReviewController.Post(new Models.Review()
            {
                ArticleId = 1,
                ReviewContent = testReviewContentData,
                Reviewer = "John Doe"
            });

            var reviewList = testHelper.ReviewController.Get().ToList();

            Assert.AreEqual(0, reviewList.Count, "Because we couldn't add a record, it must be 0.");
        }
    }
}