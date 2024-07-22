using System;
using System.IO;
using Cysharp.Threading.Tasks;
using Project.Framework.Utils;
using SFB;

namespace Project.Runtime.OutGame.UseCase
{
    public static class SFBWrapper
    {
        public static async UniTask<bool> Save(string textName, string contents)
        {
            string downloadsPath;
            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                downloadsPath = Environment.GetEnvironmentVariable("HOME") + "/Downloads";
            }
            else
            {
                downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads\";
            }

            var path = Path.Combine(downloadsPath, $"{textName}.txt");

            if (string.IsNullOrWhiteSpace(path))
            {
                CustomDebug.LogError($"[{nameof(SFBWrapper)}] パスが空です。, TextName: {textName}, Contents: {contents}");
                return false;
            }

            try
            {
                await File.WriteAllTextAsync(path, contents);
                System.Diagnostics.Process.Start(downloadsPath);
                return true;
            }
            catch (Exception e)
            {
                CustomDebug.LogError(
                    $"[{nameof(SFBWrapper)}] ファイルの保存に失敗しました。, Exception: {e} TextName: {textName}, Contents: {contents}");
                return false;
            }
        }

        public static bool Open(out string path)
        {
            var paths = StandaloneFileBrowser.OpenFilePanel("zawa", "", "txt", false);
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