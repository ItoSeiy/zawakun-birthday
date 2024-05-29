using DG.Tweening;
using Project.Framework.Project.Framework.Const;
using NaughtyAttributes;
using UnityEngine;

namespace Project.Framework.OutGame
{
    [AddComponentMenu(AddComponentMenuConst.ButtonBath + nameof(SimpleButton))]
    public sealed class SimpleButton : BaseButtonWithLongTapGesture
    {
        [Header("===ボタンのアニメーションのパラメータ===")]
        [Header("アニメーションをさせるかどうか    ")]
        [SerializeField]
        private bool _doAnimation = true;

        [SerializeField]
        [Header("ボタンをアニメーションさせるTransform")]
        [ShowIf(nameof(_doAnimation))]
        private Transform _buttonAnimationTransform;

        [SerializeField]
        [Header("押下後のスケール")]
        [ShowIf(nameof(_doAnimation))]
        private float _buttonAnimationPressScale = 0.9f;

        [SerializeField]
        [Header("完全に押下するまでの時間")]
        [ShowIf(nameof(_doAnimation))]
        private float _buttonAnimationPressDuration = 0.25f;

        [SerializeField]
        [Header("押下時のイージング")]
        [ShowIf(nameof(_doAnimation))]
        private Ease _buttonAnimationPressEase = Ease.OutCubic;

        [SerializeField]
        [Header("元の大きさに戻るまでの時間")]
        [ShowIf(nameof(_doAnimation))]
        private float _buttonAnimationPullDuration = 0.25f;

        [SerializeField]
        [Header("デフォルトのスケール")]
        [ShowIf(nameof(_doAnimation))]
        private float _buttonAnimationDefaultScale = 1f;

        [SerializeField]
        [Header("元の大きさの戻る時のイージング")]
        [ShowIf(nameof(_doAnimation))]
        private Ease _buttonAnimationPullEase = Ease.OutBounce;

#if UNITY_EDITOR
        protected override void Reset()
        {
            base.Reset();
            _buttonAnimationTransform ??= GetComponent<Transform>();
        }
#endif

        protected override void OnPressed(bool isPressed)
        {
            if (!_doAnimation)
            {
                return;
            }

            var go = _buttonAnimationTransform.gameObject;
            if (isPressed)
            {
                _buttonAnimationTransform.DOScale(_buttonAnimationPressScale, _buttonAnimationPressDuration)
                    .SetEase(_buttonAnimationPressEase).SetLink(go);
            }
            else
            {
                _buttonAnimationTransform.DOScale(_buttonAnimationDefaultScale, _buttonAnimationPullDuration)
                    .SetEase(_buttonAnimationPullEase).SetLink(go);
            }
        }
    }
}