namespace PersonalBlog.App
{
    /// <summary>
    /// Represents the setting of blog. It is loading from appsettings.json
    /// </summary>
    public class BlogSettings
    {
        /// <summary>
        /// Gets or sets the blog title.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the blog description.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the username for admin.
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the password for admin.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Gets or sets an app secret string.
        /// </summary>
        public string AppSecret { get; set; }
        /// <summary>
        /// Gets or sets the categories of post.
        /// </summary>
        public string[] Categories { get; set; }
    }
}
