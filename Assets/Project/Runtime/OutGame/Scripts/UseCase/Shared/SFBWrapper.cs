using SFB;

namespace Project.Runtime.OutGame.UseCase
{
    public static class SFBWrapper
    {
        public static bool Open(out string path)
        {
            var paths = StandaloneFileBrowser.OpenFilePanel("Title", "", "txt", false);
            if (paths.Length > 0)
            {
                path = paths[0];
                return true;
            }

            path = string.Empty;
            return false;
        }
    }
}