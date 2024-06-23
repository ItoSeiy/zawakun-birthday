using System.Collections;
using System.Collections.Generic;
using Project.Development.OutGame;
using Project.Framework.Utils;
using Project.Runtime.OutGame.UseCase;
using Project.Runtime.OutGame.View;
using UnityEngine;

namespace Project.Development
{
    public class SplashPageDevelopment : AppViewDevelopment<SplashView, SplashViewState>
    {
        [SerializeField]
        private bool _setUserData;

        protected override bool UseLocalization { get; }

        protected override void InitializeView(SplashView view)
        {
            if (_setUserData)
            {
                PlayerPrefs.SetString(PlayerPrefsConst.UserKey, "ざわ君");
            }
            else
            {
                PlayerPrefs.DeleteKey(PlayerPrefsConst.UserKey);
            }
        }

        protected override void ViewDidSetup(SplashViewState state)
        {
            var userName = PlayerPrefs.GetString(PlayerPrefsConst.UserKey, string.Empty);
            if (string.IsNullOrWhiteSpace(userName))
            {
                CustomDebug.Log($"ログイン画面へ遷移");
            }
            else
            {
                CustomDebug.Log($"ステージ選択画面へ遷移, UserName: {userName}");
            }
        }
    }
}
