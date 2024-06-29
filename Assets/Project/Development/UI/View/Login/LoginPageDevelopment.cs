using System;
using Cysharp.Threading.Tasks;
using Project.Development.OutGame;
using Project.Framework.Utils;
using Project.Runtime.OutGame.APIGateway;
using Project.Runtime.OutGame.Model;
using Project.Runtime.OutGame.UseCase;
using Project.Runtime.OutGame.View;
using R3;
using UnityEngine;

namespace Project.Development
{
    public class LoginPageDevelopment : AppViewDevelopment<LoginView, LoginViewState>
    {
        [SerializeField]
        private LetterContents _letterContents;

        private LoginUseCase _useCase;

        private LetterContents.ContentsParent _contentsParent;
        private LetterContents.ContentsParent.Contents _contents;

        protected override bool UseLocalization { get; }

        protected override void InitializeView(LoginView view)
        {
            var fileApiGateway = new FileApiGateway();
            _useCase = new LoginUseCase(fileApiGateway);

            _contentsParent = _letterContents.GetContentsParent(ContentsParentType.Login);
            _contents = _contentsParent.GetContents(ContentsType.LoginCheck);
        }

        protected override async void ViewDidSetup(LoginViewState state)
        {
            state.IsLetterActive.Value = false;
            state.IsPostActive.Value = false;

            await ActiveLetterAsync(state);

            state.OnLetterClicked.SubscribeAwait(async (_, _) =>
            {
                state.IsLetterActive.Value = false;
                await ExecuteCurrentLetterContentsAsync(state);
            }).AddTo(this);

            state.OnPostClicked.SubscribeAwait(async (_, _) =>
            {
                state.IsPostActive.Value = false;
                await ExecuteCurrentPostContentsAsync(state);
            }).AddTo(this);
        }

        private async UniTask ActiveLetterAsync(LoginViewState state)
        {
            await UniTask.WaitForSeconds(_contents.WaitForSeconds);
            state.IsLetterActive.Value = true;
        }

        private async UniTask ExecuteCurrentLetterContentsAsync(LoginViewState state)
        {
            switch (_contents.ContentsType)
            {
                case ContentsType.LoginCheck:
                {
                    await SFBWrapper.Save(_contents.Title, _contents.Text);

                    _contents = _contentsParent.GetContents(ContentsType.LoginGreeting);
                    break;
                }
                case ContentsType.LoginGreeting:
                {
                    await SFBWrapper.Save(_contents.FallBackTextTitle, _contents.FallBackText);
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }

            state.IsPostActive.Value = true;
        }

        private async UniTask ExecuteCurrentPostContentsAsync(LoginViewState state)
        {
            switch (_contents.ContentsType)
            {
                case ContentsType.LoginCheck:
                {
                    CustomDebug.LogError($"ポストは押せない。");
                    break;
                }
                case ContentsType.LoginGreeting:
                {
                    var success = await _useCase.OpenLoginCheckText(_contents.MatchPattern);
                    if (success)
                    {
                        CustomDebug.Log($"問題画面へ遷移");
                    }
                    else
                    {
                        CustomDebug.Log($"再度、ログイン確認");

                        state.IsPostActive.Value = true;
                        state.IsLetterActive.Value = true;
                    }

                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}