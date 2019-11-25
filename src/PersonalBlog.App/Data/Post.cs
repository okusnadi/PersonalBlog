using System;
using System.ComponentModel.DataAnnotations;

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
        [Required]
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        [Required]
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
        /// Gets or sets the category name.
        /// </summary>
        [Required]
        public string Category { get; set; }
    }
}
