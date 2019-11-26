namespace PersonalBlog.App.Settings
{
    /// <summary>
    /// The extensions for instance of <see cref="ISettingManager"/>.
    /// </summary>
    public static class SettingManagerExtensions
    {
        /// <summary>
        /// Get <see cref="GetSystemSetting"/> from instance of <see cref="ISettingManager"/>.
        /// </summary>
        /// <param name="manager">The instance of <see cref="ISettingManager"/>.</param>
        /// <returns>The instance of <see cref="GetSystemSetting"/> or null.</returns>
        public static SystemSetting GetSystemSetting(this ISettingManager manager)
            => manager.Get<SystemSetting>();
    }
}
