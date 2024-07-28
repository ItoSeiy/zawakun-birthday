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
                3 => ContentsType.Question4Save,
                _ => throw new ArgumentOutOfRangeException()
            };

            _contentsParent = letterContents;
            _contents = _contentsParent.GetContents(contentType);

            CustomDebug.Log($"問題Index: {progress} Type: {_contents.ContentsType}");
        }

        protected override UniTask ViewDidSetup(PostLetterViewState state)
        {
            state.IsLetterActive.Value = false;
            state.IsPostActive.Value = false;

            SetupInternal(state).Forget();

            return UniTask.CompletedTask;
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
                case ContentsType.Question1Save:
                {
                    await SFBWrapper.Save(_contents.Title, _contents.Text);
                    _contents = _contentsParent.GetContents(ContentsType.Question1Open);
                    break;
                }
                case ContentsType.Question2Save:
                {
                    PlayerPrefs.SetInt(PlayerPrefsConst.Int.QuestionProgress, 1);
                    await SFBWrapper.Save(_contents.Title, _contents.Text);
                    _contents = _contentsParent.GetContents(ContentsType.Question2Open);
                    break;
                }
                case ContentsType.Question3Save:
                {
                    PlayerPrefs.SetInt(PlayerPrefsConst.Int.QuestionProgress, 2);
                    await SFBWrapper.Save(_contents.Title, _contents.Text);
                    _contents = _contentsParent.GetContents(ContentsType.Question3Open);
                    break;
                }
                case ContentsType.Question4Save:
                {
                    PlayerPrefs.SetInt(PlayerPrefsConst.Int.QuestionProgress, 3);
                    await SFBWrapper.Save(_contents.Title, _contents.Text);
                    _contents = _contentsParent.GetContents(ContentsType.Question4Open);
                    break;
                }
                case ContentsType.QuestionClearSave:
                {
                    await SFBWrapper.Save(_contents.Title, _contents.Text);
                    _contents = _contentsParent.GetContents(ContentsType.QuestionClearOpenFailed);
                    break;
                }
                case ContentsType.Question1Open:
                case ContentsType.Question2Open:
                case ContentsType.Question3Open:
                case ContentsType.Question4Open:
                case ContentsType.QuestionClearOpenFailed:
                {
                    await SFBWrapper.Save(_contents.Title, _contents.Text);
                    break;
                }
                case ContentsType.QuestionClearOpenSuccess:
                {
                    await SFBWrapper.Save(_contents.Title, _contents.Text);
                    return;
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

                        CustomDebug.Log($"Question1 問題正解。次の問題");
                    }
                    else
                    {
                        await (ActiveLetterAsync(state), ActivePostAsync(state));

                        CustomDebug.Log($"Question1 問題不正解。再度、問題を解く");
                    }

                    break;
                }
                case ContentsType.Question2Open:
                {
                    var success = await _questionUseCase.OpenAnswerText(_contents.MatchPattern);
                    if (success)
                    {
                        _contents = _contentsParent.GetContents(ContentsType.Question3Save);
                        ActiveLetterAsync(state).Forget();

                        CustomDebug.Log($"Question2 問題正解。次の問題");
                    }
                    else
                    {
                        await (ActiveLetterAsync(state), ActivePostAsync(state));

                        CustomDebug.Log($"Question2 問題不正解。再度、問題を解く");
                    }

                    break;
                }
                case ContentsType.Question3Open:
                {
                    var success = await _questionUseCase.OpenAnswerText(_contents.MatchPattern);
                    if (success)
                    {
                        _contents = _contentsParent.GetContents(ContentsType.Question4Save);
                        ActiveLetterAsync(state).Forget();

                        CustomDebug.Log($"Question3 問題正解。次の問題");
                    }
                    else
                    {
                        await (ActiveLetterAsync(state), ActivePostAsync(state));

                        CustomDebug.Log($"Question3 問題不正解。再度、問題を解く");
                    }

                    break;
                }
                case ContentsType.Question4Open:
                {
                    var success = await _questionUseCase.OpenAnswerText(_contents.MatchPattern);
                    if (success)
                    {
                        _contents = _contentsParent.GetContents(ContentsType.QuestionClearSave);
                        ActiveLetterAsync(state).Forget();

                        CustomDebug.Log($"Question4 問題正解。正解画面に遷移。");
                    }
                    else
                    {
                        await (ActiveLetterAsync(state), ActivePostAsync(state));

                        CustomDebug.Log($"Question4 問題不正解。再度、問題を解く");
                    }

                    break;
                }
                case ContentsType.QuestionClearOpenFailed:
                {
                    var success = await _questionUseCase.OpenAnswerText(_contents.MatchPattern);
                    if (success)
                    {
                        PlayerPrefs.SetInt(PlayerPrefsConst.Int.QuestionProgress, 0);

                        _contents = _contentsParent.GetContents(ContentsType.QuestionClearOpenSuccess);
                        await (ActiveLetterAsync(state), ActivePostAsync(state));

                        CustomDebug.Log($"データリセット");
                    }
                    else
                    {
                        _contents = _contentsParent.GetContents(ContentsType.QuestionClearOpenFailed);
                        await (ActiveLetterAsync(state), ActivePostAsync(state));

                        CustomDebug.Log("データリセットしない");
                    }

                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}