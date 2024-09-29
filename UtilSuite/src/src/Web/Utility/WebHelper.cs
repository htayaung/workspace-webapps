using Ganss.Xss;

namespace Web.Utility
{
    public class WebHelper
    {
        public static string SanitizeHtml(string html)
        {
            var sanitizer = new HtmlSanitizer();
            return sanitizer.Sanitize(html);
        }
    }
}
