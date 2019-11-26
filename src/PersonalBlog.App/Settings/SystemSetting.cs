using System.ComponentModel.DataAnnotations;

namespace PersonalBlog.App.Settings
{
    /// <summary>
    /// Represents the setting of blog. It is loading from appsettings.json
    /// </summary>
    public class SystemSetting:ISetting
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
        /// Gets or sets the culture.
        /// </summary>
        public string Culture { get; set; }

        /// <summary>
        /// Ggets or sets the version.
        /// </summary>
        public string Version { get; set; }

        public string Name => "System";

        public ISetting Initialize() => new SystemSetting
        {
            Title = "PersonalBlog",
            Description = "A personal blog by .NET CORE Blazor.",
            AppSecret = "oi23intf0924hgfowirgw0293rf",
            Culture = "en-us",
            UserName = "admin",
            Password = "123456"
        };
    }
}
