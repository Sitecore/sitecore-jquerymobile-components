namespace SitecoreMobile.Common
{
    public static class StringExtensions
    {
        public static string PrefixWith(this string value, string prefix)
        {
            return prefix + value;
        }
    }
}