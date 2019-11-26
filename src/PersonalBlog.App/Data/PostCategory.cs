using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PersonalBlog.App.Cultures;

namespace PersonalBlog.App.Data
{
    /// <summary>
    /// The category of post.
    /// </summary>
    public class PostCategory
    {
        /// <summary>
        /// Get or sets the id of category, it must be identity.
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Display(ResourceType = typeof(Language), Name = nameof(Language.Field_Post_Category_Name))]
        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = nameof(Language.Validate_Field_Required))]
        [StringLength(60, ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = nameof(Language.Validate_Field_StringLength))]
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the sorting number of category, the smaller is in front.
        /// </summary>
        public int SortNo { get; set; }

        /// <summary>
        /// Gets or sets the post collection for this category.
        /// </summary>
        public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();
    }
}
