using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Project.Framework.OutGame;
using Project.Framework.Utils;
using Project.Runtime.OutGame.Model;
using Project.Runtime.OutGame.UseCase;
using Project.Runtime.OutGame.View;
using R3;

namespace Project.Runtime.OutGame.Presentation
{
    /// <summary>
    ///     ゲーム進行のPresenter
    /// </summary>
    public sealed class LoginPagePresenter : PagePresenterBase<PostLetterPage, PostLetterView, PostLetterViewState>
    {
        private readonly LoginUseCase _loginUseCase;

        private readonly LetterContents.ContentsParent _contentsParent;
        private LetterContents.ContentsParent.Contents _contents;

        public LoginPagePresenter(PostLetterPage view, ITransitionService transitionService, LoginUseCase loginUseCase,
            LetterContents.ContentsParent letterContents) : base(view, transitionService)
        {
            _loginUseCase = loginUseCase;

            _contentsParent = letterContents;
            _contents = _contentsParent.GetContents(ContentsType.LoginCheck);
        }

        protected override async UniTask ViewDidSetup(PostLetterViewState state)
        {
            state.IsLetterActive.Value = false;
            state.IsPostActive.Value = false;

            SetupInternal(state).Forget();

            await UniTask.CompletedTask;
        }

        private async UniTaskVoid SetupInternal(PostLetterViewState state)
        {
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

        private async UniTask ActiveLetterAsync(PostLetterViewState state)
        {
            await UniTask.WaitForSeconds(_contents.WaitForSeconds);
            state.IsLetterActive.Value = true;
        }

        private async UniTask ActivePostAsync(PostLetterViewState state)
        {
            await UniTask.WaitForSeconds(_contents.WaitForSeconds);
            state.IsPostActive.Value = true;
        }

        private async UniTask ExecuteCurrentLetterContentsAsync(PostLetterViewState state)
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
                    await SFBWrapper.Save(_contents.Title, _contents.Text);
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }

            state.IsPostActive.Value = true;
        }

        private async UniTask ExecuteCurrentPostContentsAsync(PostLetterViewState state)
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
                    var success = await _loginUseCase.OpenLoginCheckText(_contents.MatchPattern);
                    if (success)
                    {
                        state.InvokeFinish();
                        await UniTask.WaitForSeconds(0.5f);

                        TransitionService.PushQuestionPage();

                        CustomDebug.Log($"問題画面へ遷移");
                    }
                    else
                    {
                        CustomDebug.Log($"再度、ログイン確認");

                        await (ActiveLetterAsync(state), ActivePostAsync(state));
                    }

                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}