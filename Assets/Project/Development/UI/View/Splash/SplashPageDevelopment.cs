using System.Collections;
using System.Collections.Generic;
using Project.Development.OutGame;
using Project.Framework.Utils;
using Project.Runtime.OutGame.UseCase;
using Project.Runtime.OutGame.View;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Development
{
    public class SplashPageDevelopment : AppViewDevelopment<SplashView, SplashViewState>
    {
        [SerializeField]
        private bool _setLoggedIn;

        protected override bool UseLocalization { get; }

        protected override void InitializeView(SplashView view)
        {
            if (_setLoggedIn)
            {
                PlayerPrefs.SetInt(PlayerPrefsConst.Bool.IsLoggedIn, 1);
            }
            else
            {
                PlayerPrefs.DeleteKey(PlayerPrefsConst.Bool.IsLoggedIn);
            }
        }

        protected override void ViewDidSetup(SplashViewState state)
        {
            var loggedInInt = PlayerPrefs.GetInt(PlayerPrefsConst.Bool.IsLoggedIn, 0);
            var isLoggedIn = loggedInInt == 1;

            CustomDebug.Log(isLoggedIn ? "ステージ選択画面へ繊維" : "ログイン画面へ遷移");
        }
    }
}