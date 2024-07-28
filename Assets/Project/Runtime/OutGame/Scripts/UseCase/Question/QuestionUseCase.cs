using System.Text.RegularExpressions;
using Cysharp.Threading.Tasks;
using Project.Framework.Utils;
using Project.Runtime.OutGame.APIGateway;
using UnityEngine;

namespace Project.Runtime.OutGame.UseCase
{
    public class QuestionUseCase
    {
        private readonly FileApiGateway _apiGateway;

        public QuestionUseCase(FileApiGateway apiGateway)
        {
            _apiGateway = apiGateway;
        }

        public async UniTask<bool> OpenAnswerText(string pattern)
        {
            if (SFBWrapper.Open(out var path))
            {
                var text = await _apiGateway.GetFileText(path);

                var match = Regex.Match(text, pattern);
                if (!match.Success)
                {
                    return false;
                }

                CustomDebug.Log($"[{nameof(QuestionUseCase)}] 問題をクリア。");
                return true;
            }

            CustomDebug.LogWarning($"[{nameof(QuestionUseCase)}] パス指定がなかったため、読み込まない");
            return false;
        }
    }
}