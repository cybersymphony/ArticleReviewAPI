using ArticleReviewAPI.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleReviewAPI.Models
{
    public class Review : IValidatable, ICloneable, IOverwritable, IDeepClonable, IReportable
    {
        #region Properties
        [Required]
        public int Id { get; set; }
        [Required]
        public int ArticleId { get; set; }
        [Required]
        public string Reviewer { get; set; }
        [Required]
        public string ReviewContent { get; set; }
        #endregion

        #region Implementations
        /// <summary>
        /// Shallow clone.
        /// </summary>
        /// <returns>shallow cloned object.</returns>
        public object Clone()
        {
            return new Review()
            {
                Id = this.Id,
                ArticleId = this.ArticleId,
                Reviewer = this.Reviewer,
                ReviewContent = this.ReviewContent
            };
        }
        /// <summary>
        /// Deep copying.
        /// </summary>
        /// <returns>Deeply cloned object</returns>
        public object DeepClone()
        {
            return new Review()
            {
                Id = this.Id,
                ArticleId = this.ArticleId,
                Reviewer = (string)this.Reviewer.Clone(),
                ReviewContent = (string)this.ReviewContent.Clone()
            };
        }
        /// <summary>
        /// Overwrites the given model to current object.
        /// </summary>
        /// <param name="model">Object that properties copied from.</param>
        public void Overwrite(object model)
        {
            Review mod = (Review)model;
            Id = mod.Id;
            ArticleId = mod.ArticleId;
            Reviewer = mod.Reviewer;
            ReviewContent = mod.ReviewContent;
        }
        /// <summary>
        /// Gets objects non-sensitive data as a string
        /// </summary>
        /// <returns>Objects properties and its values.</returns>
        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(nameof(Id) + " : " + Id + ", ");
            sb.AppendLine(nameof(ArticleId) + " : " + ArticleId + ", ");
            sb.AppendLine(nameof(Reviewer) + " : " + Reviewer + ", ");
            sb.AppendLine(nameof(ReviewContent) + " : " + ReviewContent + ", ");
            return sb.ToString();
        }

        /// <summary>
        /// Checks weather the model is valid or not.
        /// </summary>
        /// <returns>If model is a valid object, returns true. Otherwise it returns false</returns>
        public bool ValidateModel()
        {
            bool thereIsError = string.IsNullOrWhiteSpace(Reviewer) ||
                    string.IsNullOrWhiteSpace(ReviewContent) ||
                    this.ArticleId < 0;

            return !thereIsError;
        }
        #endregion
    }
}
