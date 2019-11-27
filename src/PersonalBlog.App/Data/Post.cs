using System;
using System.ComponentModel.DataAnnotations;
using PersonalBlog.App.Cultures;

namespace PersonalBlog.App.Data
{
    /// <summary>
    /// Defines the entity of post.
    /// </summary>
    public class Post
    {
        /// <summary>
        /// Gets or sets the id of blog.
        /// </summary>
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [Display(ResourceType = typeof(Language), Name = nameof(Language.Field_Post_Title))]
        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = nameof(Language.Validate_Field_Required))]
        [StringLength(100, ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = nameof(Language.Validate_Field_StringLength))]
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        [Display(ResourceType = typeof(Language), Name = nameof(Language.Field_Post_Content))]
        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = nameof(Language.Validate_Field_Required))]
        public string Content { get; set; }
        /// <summary>
        /// Gets or sets the created time of blog.
        /// </summary>
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        /// <summary>
        /// Determined whether this article at the top of list.
        /// </summary>
        public bool IsTop { get; set; }
        /// <summary>
        /// Gets the view counts.
        /// </summary>
        public int ViewCount { get; private set; }

        /// <summary>
        /// Gets or sets the category id.
        /// </summary>
        [Display(ResourceType = typeof(Language), Name = nameof(Language.Field_Post_Category_Name))]
        [Required(ErrorMessageResourceType =typeof(Language),ErrorMessageResourceName =nameof(Language.Validate_Field_Required))]
        public string CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the category name.
        /// </summary>
        public virtual PostCategory Category { get; set; }

        /// <summary>
        /// Set the view count with specified offset
        /// </summary>
        /// <param name="offset">The offset to increase while each view in post.</param>
        public void SetViewCount(int offset=1)
            => ViewCount += offset;
    }
}
