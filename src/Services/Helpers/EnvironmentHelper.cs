using System.IO;

namespace src.Services.Helpers
{
    public class EnvironmentHelper
    {
        public static string WebRootPath { get; set; }
        public static string AttachmentPath => Path.Combine(WebRootPath, FilePath);
        public static string FilePath => "files";
    }
}
