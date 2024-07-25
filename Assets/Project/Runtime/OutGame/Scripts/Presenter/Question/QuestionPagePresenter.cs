using System;
using Cysharp.Threading.Tasks;
using PlasticGui.WorkspaceWindow.CodeReview.ReviewChanges.Summary;
using Project.Framework.OutGame;
using Project.Framework.Utils;
using Project.Runtime.OutGame.Model;
using Project.Runtime.OutGame.UseCase;
using Project.Runtime.OutGame.View;
using R3;
using UnityEngine;

namespace Project.Runtime.OutGame.Presentation
{
    /// <summary>
    /// 問題のPresenter
    /// </summary>
    public sealed class QuestionPagePresenter : PagePresenterBase<PostLetterPage, PostLetterView, PostLetterViewState>
    {
        private readonly QuestionUseCase _questionUseCase;

        private readonly LetterContents.ContentsParent _contentsParent;
        private LetterContents.ContentsParent.Contents _contents;

        public QuestionPagePresenter(PostLetterPage view, ITransitionService transitionService,
            QuestionUseCase questionUseCase, LetterContents.ContentsParent letterContents) : base(view,
            transitionService)
        {
            _questionUseCase = questionUseCase;

            var progress = PlayerPrefs.GetInt(PlayerPrefsConst.Int.QuestionProgress);

            var contentType = progress switch
            {
                0 => ContentsType.Question1Save,
                1 => ContentsType.Question2Save,
                2 => ContentsType.Question3Save,
                4 => ContentsType.Question4Save,
                _ => throw new ArgumentOutOfRangeException()
            };

            _contentsParent = letterContents;
            _contents = _contentsParent.GetContents(contentType);
        }

        protected override async UniTask ViewDidSetup(PostLetterViewState state)
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

        private async UniTask ActiveLetterAsync(PostLetterViewState state)
        {
            await UniTask.WaitForSeconds(_contents.WaitForSeconds);
            state.IsLetterActive.Value = true;
        }

        private async UniTask ExecuteCurrentLetterContentsAsync(PostLetterViewState state)
        {
            switch (_contents.ContentsType)
            {
                case ContentsType.Question1Save:
                {
                    await SFBWrapper.Save(_contents.Title, _contents.Text);
                    _contents = _contentsParent.GetContents(ContentsType.Question1Open);
                    break;
                }
                case ContentsType.Question2Save:
                {
                    await SFBWrapper.Save(_contents.Title, _contents.Text);
                    _contents = _contentsParent.GetContents(ContentsType.Question2Open);
                    break;
                }
                case ContentsType.Question3Save:
                {
                    await SFBWrapper.Save(_contents.Title, _contents.Text);
                    _contents = _contentsParent.GetContents(ContentsType.Question3Open);
                    break;
                }
                case ContentsType.Question4Save:
                {
                    await SFBWrapper.Save(_contents.Title, _contents.Text);
                    _contents = _contentsParent.GetContents(ContentsType.Question4Open);
                    break;
                }
                case ContentsType.Question1Open:
                case ContentsType.Question2Open:
                case ContentsType.Question3Open:
                case ContentsType.Question4Open:
                {
                    await SFBWrapper.Save(_contents.FallBackTextTitle, _contents.FallBackText);
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
                case ContentsType.Question1Save:
                case ContentsType.Question2Save:
                case ContentsType.Question3Save:
                case ContentsType.Question4Save:
                {
                    CustomDebug.LogError($"ポストは押せない。Type: {_contents.ContentsType}");
                    break;
                }
                case ContentsType.Question1Open:
                {
                    var success = await _questionUseCase.OpenAnswerText(_contents.MatchPattern);
                    if (success)
                    {
                        _contents = _contentsParent.GetContents(ContentsType.Question2Save);
                        ActiveLetterAsync(state).Forget();

                        CustomDebug.Log($"問題正解。次の問題");
                    }
                    else
                    {
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