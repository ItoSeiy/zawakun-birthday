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

                var progress = PlayerPrefs.GetInt(PlayerPrefsConst.Int.QuestionProgress);

                if (progress == 3)
                {
                    CustomDebug.Log($"[{nameof(QuestionUseCase)}] すべてクリアした。");
                    PlayerPrefs.SetInt(PlayerPrefsConst.Bool.IsClearedAll, 1);
                    return true;
                }

                CustomDebug.Log($"[{nameof(QuestionUseCase)}] 問題をクリア 進捗Index: Before{progress} After:{progress + 1}");
                progress++;
                PlayerPrefs.SetInt(PlayerPrefsConst.Int.QuestionProgress, progress);
                return true;
            }

            CustomDebug.LogWarning($"[{nameof(QuestionUseCase)}] パス指定がなかったため、読み込まない");
            return false;
        }
    }
}