using System;

namespace PersonalBlog.App.Settings
{
    /// <summary>
    /// Represent the setting.
    /// </summary>
    public interface ISetting
    {
        /// <summary>
        /// The name of setting.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// The initialize operation for this setting.
        /// </summary>
        /// <returns>The instance with default values.</returns>
        ISetting Initialize();
    }
}
