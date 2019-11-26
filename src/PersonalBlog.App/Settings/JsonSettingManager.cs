using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.App.Settings
{
    /// <summary>
    /// Use json format to manage the setting.
    /// </summary>
    public class JsonSettingManager : ISettingManager
    {
        private readonly IMemoryCache _cache;
        private readonly IHostEnvironment _env;
        public const string CONFIGURATION_PATH = "settings";
        /// <summary>
        /// Initialize the instance of <see cref="JsonSettingManager"/> as a class.
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="environment"></param>
        public JsonSettingManager(IMemoryCache cache,IHostEnvironment environment)
        {
            _cache = cache;
            _env = environment;
        }

        /// <summary>
        /// Get the specify setting. The first level will get from memory cache, if the cache not exist, then get from physical path by intenal directory.
        /// </summary>
        /// <typeparam name="TConfiguration">The type of setting.</typeparam>        
        /// <returns>The setting from cache or physical path.</returns>
        public virtual TConfiguration Get<TConfiguration>()
            where TConfiguration : class, ISetting,new()
        {
            var setting = new TConfiguration();

            if(_cache.TryGetValue(setting.Name,out TConfiguration cacheConfiguration))
            {
                return cacheConfiguration;
            }

            var path = GetPath(setting);

            try
            {
                using var reader = new StreamReader(path, Encoding.UTF8);
                var content = reader.ReadToEnd();
                setting = JsonConvert.DeserializeObject<TConfiguration>(content);
                _cache.Set(setting.Name, setting); //refresh
            }
            catch (FileNotFoundException)
            {
                setting = (TConfiguration)setting.Initialize();
                Set(setting);
            }
            return setting;
        }

        /// <summary>
        /// Set the specify setting as json file and refresh the cache.
        /// </summary>
        /// <typeparam name="TConfiguration">The type of setting.</typeparam>        
        /// <param name="setting">The setting to save.</param>
        public virtual void Set<TConfiguration>(TConfiguration setting)
            where TConfiguration : class, ISetting, new()
        {
            if (setting == default)
            {
                throw new ArgumentNullException(nameof(setting));
            }

            var configJsonStr = JsonConvert.SerializeObject(setting);
            var savedPath = GetPath(setting);

            using (var writer = new StreamWriter(savedPath, false, Encoding.UTF8))
            {
                writer.WriteLine(configJsonStr);
                writer.Flush();
            }

            _cache.Set(setting.Name, setting);
        }

        /// <summary>
        /// Get the saved physical path of setting json file. If the directory not exist, it will be created automatically.        
        /// </summary>
        /// <typeparam name="TConfiguration">The type of setting.</typeparam>
        /// <param name="setting">The setting to saved by <see cref="ISetting.Name"/> for file name.</param>
        /// <returns>The physical path of setting saved in disk of this program.</returns>
        public virtual string GetPath<TConfiguration>(TConfiguration setting) 
            where TConfiguration : class, ISetting, new()
        {
            var path = Path.Combine(_env.ContentRootPath, CONFIGURATION_PATH);
            var file = string.Concat(setting.Name, ".json");

            var directoryInfo = new DirectoryInfo(path);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
            return Path.Combine(path, file);
        }
    }
}
