namespace Repositories.Database
{
    using System.Configuration;

    internal static class Connection
    {
        public static string String
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("MONGOLAB_URI");
            }
        }
    }
}
