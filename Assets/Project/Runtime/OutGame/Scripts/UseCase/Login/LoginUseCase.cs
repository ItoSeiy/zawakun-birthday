using System.IO;
using System.Text.RegularExpressions;
using Cysharp.Threading.Tasks;
using Project.Framework.Utils;
using Project.Runtime.OutGame.APIGateway;
using Project.Runtime.OutGame.Model;
using UnityEngine;

namespace Project.Runtime.OutGame.UseCase
{
    public class LoginUseCase
    {
        private readonly UserModel _model;
        private readonly FileApiGateway _apiGateway;

        public LoginUseCase(UserModel model, FileApiGateway fileApiGateway)
        {
            _model = model;
            _apiGateway = fileApiGateway;
        }

        public async UniTask SaveEnterUserNameLetterAsync(string contents)
        {
            if (SFBWrapper.Save(out var path))
            {
                await File.WriteAllTextAsync(path, contents);
            }
        }

        public async UniTask FetchUserModelAsync(string pattern, string fallBackContents)
        {
            if (SFBWrapper.Open(out var path))
            {
                var text = await _apiGateway.GetFileText(path);

                // 名前判定
                var match = Regex.Match(text, pattern);

                if (match.Success)
                {
                    _model.SetValue(text);
                    PlayerPrefs.SetString(PlayerPrefsConst.UserKey, _model.Name);
                }
                else
                {
                    
                }
            }
            else
            {
                CustomDebug.LogWarning($"[{nameof(LoginUseCase)}] パス指定がなかったため、読み込まない");
            }
        }

        public async UniTask Greeting(string contents)
        {
            if (SFBWrapper.Save(out var path)) ;
            {
                await File.WriteAllTextAsync(path, contents);
            }
        }
    }
}