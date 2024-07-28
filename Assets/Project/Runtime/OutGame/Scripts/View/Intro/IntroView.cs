using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Project.Framework.Extensions;
using Project.Framework.OutGame;
using R3;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Project.Runtime.OutGame.View
{
    /// <summary>
    /// 導入のView
    /// </summary>
    public sealed class IntroView : AppView<IntroViewState>
    {
        [SerializeField]
        private SimpleButton _introButton;

        [SerializeField]
        private Text _introText;

        [SerializeField]
        private Animation _textAnim;

        [SerializeField]
        private AudioSource _seAudioSource;

        [SerializeField]
        private AudioClip _textAudioClip;

        [SerializeField]
        private AudioClip _postAudioClip;

        private int _textIndex = -1;

        private IntroViewState _state;

        private bool _isPlaying;

        private readonly string[] _textArray =
        {
            "家に謎のアタッシュケースが届いた小澤は" +
            $"{Environment.NewLine}" +
            "そこに記されていた中州の蟹差洲亭を訪れた",

            "おかしい・・・？" +
            $"{Environment.NewLine}" +
            "店内に灯りがついているにも関わらず締まっている",

            "鍵がしまっているのか開きそうにはない",

            "〝ガタッ〟",

            "様子をうかがっていると扉の横のポストから" +
            $"{Environment.NewLine}" +
            "ポストが開いたような音がした。",

            "見てみると手紙が落ちている。。。" +
            $"{Environment.NewLine}" +
            "見てみると手紙には小澤さんへと書かれている。。。",

            "手紙を読んでいい物か迷いつつ気になってしまった小澤は" +
            $"{Environment.NewLine}" +
            "手紙を読んでみることにした・・・"
        };

        protected override UniTask<IntroViewState> Setup()
        {
            var state = new IntroViewState();

            _state = state;
            _introButton.SetOnClickDestination(() => ShowIntro().Forget()).AddTo(this);

            ShowIntro(1.5f).Forget();

            return UniTask.FromResult(state);
        }

        private async UniTask ShowIntro(float delay = 0f)
        {
            if (_isPlaying)
            {
                return;
            }

            _isPlaying = true;
            _introText.text = string.Empty;
            _textAnim.Stop();

            _textIndex++;

            if (_textIndex >= _textArray.Length)
            {
                _state.InvokeTextFinished();
                return;
            }

            await UniTask.WaitForSeconds(delay);

            if (_textIndex == 3)
            {
                _seAudioSource.PlayOneShot(_postAudioClip);
            }

            var text = _textArray[_textIndex];
            await _introText.DOText(text, text.Length * 0.03f)
                .OnUpdate(() =>
                {
                    if (_seAudioSource.isPlaying)
                    {
                        return;
                    }

                    _seAudioSource.PlayOneShot(_textAudioClip);
                })
                .SetEase(Ease.Linear)
                .AsyncWaitForCompletion();

            _textAnim.Play();

            _isPlaying = false;
        }
    }

    public sealed class IntroViewState : AppViewState
    {
        private readonly Subject<Unit> _onTextFinished = new();

        public Observable<Unit> OnTextFinished => _onTextFinished;

        public void InvokeTextFinished()
        {
            _onTextFinished.OnNext(Unit.Default);
        }
    }
}