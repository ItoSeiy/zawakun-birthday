using System.Text.RegularExpressions;
using Cysharp.Threading.Tasks;
using Project.Framework.Utils;
using Project.Runtime.OutGame.APIGateway;
using UnityEngine;

namespace Project.Runtime.OutGame.UseCase
{
    public class LoginUseCase
    {
        private readonly FileApiGateway _apiGateway;

        public LoginUseCase(FileApiGateway fileApiGateway)
        {
            _apiGateway = fileApiGateway;
        }

        public async UniTask<bool> OpenLoginCheckText(string pattern)
        {
            if (SFBWrapper.Open(out var path))
            {
                var text = await _apiGateway.GetFileText(path);

                var match = Regex.Match(text, pattern);
                if (!match.Success)
                {
                    return false;
                }

                PlayerPrefs.SetInt(PlayerPrefsConst.Int.QuestionProgress, 0);
                PlayerPrefs.SetInt(PlayerPrefsConst.Bool.IsLoggedIn, 1);
                return true;
            }

            CustomDebug.LogWarning($"[{nameof(LoginUseCase)}] パス指定がなかったため、読み込まない");
            return false;
        }
    }
}