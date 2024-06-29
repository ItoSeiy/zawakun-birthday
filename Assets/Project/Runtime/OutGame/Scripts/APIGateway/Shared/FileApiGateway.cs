using System;
using Cysharp.Threading.Tasks;
using Project.Framework.Utils;
using UnityEngine;
using UnityEngine.Networking;

namespace Project.Runtime.OutGame.APIGateway
{
    public class FileApiGateway
    {
        public async UniTask<string> GetFileText(string path)
        {
            var webRequest = UnityWebRequest.Get(path);

            await webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                return webRequest.downloadHandler.text;
            }

            CustomDebug.LogError($"[{nameof(FileApiGateway)}]Failed to get file text from " + path);
            return await UniTask.FromResult(string.Empty);
        }
    }
}