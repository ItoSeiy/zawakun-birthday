using Cysharp.Threading.Tasks;
using Project.Framework.Utils;
using Project.Runtime.OutGame.APIGateway;
using Project.Runtime.OutGame.Model;
using SFB;
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

        public async UniTask FetchUserModel()
        {
            if (SFBWrapper.Open(out var path))
            {
                var text = await _apiGateway.GetFileText(path);
                _model.SetValue(text);
                PlayerPrefs.SetString(PlayerPrefsConst.UserKey, _model.Name);
            }
            else
            {
                CustomDebug.LogWarning($"[LoginUseCase] パス指定がなかったため、読み込まない");
            }
        }

        public void Greeting()
        {
            if(SFBWrapper.Open(out var path));
            {
                
            }
        }
    }
}