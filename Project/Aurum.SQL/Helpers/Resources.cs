using System.IO;
using System.Reflection;

namespace Aurum.SQL
{
    public class Resources
    {
        public static Stream GetDefaultTemplates()
        {
            var asm = Assembly.GetExecutingAssembly();
            var stream = asm.GetManifestResourceStream("Aurum.SQL.DefaultSqlTemplates.json");
            return stream;
        }
    }
}
