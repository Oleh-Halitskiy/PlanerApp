using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace UtilsClasses
{
    /// <summary>
    /// Util class for connection strings 
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Function for getting connection string from app.config
        /// </summary>
        /// <param name="name">The name of connections string in app.config</param>
        /// <returns>Returns string representing connection string</returns>
        public static string CnnValue(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
