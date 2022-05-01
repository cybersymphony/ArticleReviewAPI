using ArticleReviewAPI.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleReviewAPI.Models
{
    public class Article : IValidatable, ICloneable, IOverwritable, IDeepClonable, IReportable
    {
        #region Properties
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string ArticleContent { get; set; }
        [Required]
        public DateTime PublishDate { get; set; }
        [Required]
        public int StarCount { get; set; } 
        #endregion

        #region Implementations
        /// <summary>
        /// Shallow clone.
        /// </summary>
        /// <returns>shallow cloned object.</returns>
        public object Clone()
        {
            return new Article()
            {
                Id = this.Id,
                Title = this.Title,
                Author = this.Author,
                ArticleContent = this.ArticleContent,
                PublishDate = this.PublishDate,
                StarCount = this.StarCount
            };
        }
        /// <summary>
        /// Deep copying.
        /// </summary>
        /// <returns>Deeply cloned object</returns>
        public object DeepClone()
        {
            return new Article()
            {
                Id = this.Id,
                Title = (string)this.Title.Clone(),
                Author = (string)this.Author.Clone(),
                ArticleContent = (string)this.ArticleContent.Clone(),
                PublishDate = this.PublishDate,
                StarCount = this.StarCount
            };
        }
        /// <summary>
        /// Overwrites the given model to current object.
        /// </summary>
        /// <param name="model">Object that properties copied from.</param>
        public void Overwrite(object model)
        {
            Article mod = (Article)model;
            Id = mod.Id;
            Title = mod.Title;
            Author = mod.Author;
            ArticleContent = mod.ArticleContent;
            PublishDate = mod.PublishDate;
            StarCount = mod.StarCount;
        }
        /// <summary>
        /// Gets objects non-sensitive data as a string
        /// </summary>
        /// <returns>Objects properties and its values.</returns>
        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(nameof(Id) + " : " + Id + ", ");
            sb.AppendLine(nameof(Title) + " : " + Title + ", ");
            sb.AppendLine(nameof(Author) + " : " + Author + ", ");
            sb.AppendLine(nameof(ArticleContent) + " : " + ArticleContent + ", ");
            sb.AppendLine(nameof(PublishDate) + " : " + PublishDate + ", ");
            sb.AppendLine(nameof(StarCount) + " : " + StarCount + ", "); 
            return sb.ToString();
        }

        /// <summary>
        /// Checks weather the model is valid or not.
        /// </summary>
        /// <returns>If model is a valid object, returns true. Otherwise it returns false</returns>
        public bool ValidateModel()
        {
            bool thereIsError = string.IsNullOrWhiteSpace(Title) ||
                    string.IsNullOrWhiteSpace(Author) ||
                    string.IsNullOrWhiteSpace(ArticleContent) ||
                    this.StarCount < 0;

            return !thereIsError;
        }
        #endregion
    }
}
