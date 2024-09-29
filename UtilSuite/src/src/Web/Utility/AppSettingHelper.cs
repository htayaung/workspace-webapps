namespace Web.Utility
{
    public static class AppSettingHelper
    {
        private static IConfiguration _configuration;

        public static void Initialize(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }

        public static string GetApplicationName()
        {
            var value = _configuration["AppSettings:AppTitle"];
            return value ?? string.Empty;
        }
    }
}
