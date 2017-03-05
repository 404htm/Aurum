namespace Aurum.Integration.Tests.Temp.Extensions
{
    public static class NameExtensions
    {
        public static string ToSafeNameCS(this string name)
        {
            //TODO: This
            return name
                .Replace(" ", "_");
        }
    }
}
