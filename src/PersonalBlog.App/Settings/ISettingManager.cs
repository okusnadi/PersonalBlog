using System;
using System.Collections.Generic;

namespace PersonalBlog.App.Settings
{
    /// <summary>
    /// The manager to save and get the setting witch implemente <see cref="ISetting"/>.
    /// </summary>
    public interface ISettingManager
    {
        /// <summary>
        /// Set the setting with given name. If the given name exists, then the setting is updated, otherwise create a new setting.
        /// </summary>
        /// <typeparam name="TConfiguration">The type of the setting.</typeparam>
        /// <param name="setting">The setting to save.</param>
        /// <exception cref="InvalidOperationException">Save setting failed.</exception>
        void Set<TConfiguration>(TConfiguration setting) where TConfiguration : class, ISetting,new();

        /// <summary>
        /// Get the specify setting.
        /// </summary>
        /// <typeparam name="TConfiguration">The type of the setting.</typeparam>
        /// <exception cref="KeyNotFoundException">The setting not found.</exception>
        TConfiguration Get<TConfiguration>() where TConfiguration : class, ISetting,new();
    }
}
