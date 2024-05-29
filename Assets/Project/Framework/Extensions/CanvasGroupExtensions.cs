using Cysharp.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace Project.Framework.Extensions
{
    public static class CanvasGroupExtensions
    {
        public static void Enable(this CanvasGroup canvasGroup)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
        }

        public static async UniTask EnableAsync(this CanvasGroup canvasGroup, float fadeDuration)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            await canvasGroup.DOFade(1f, fadeDuration);
        }

        public static void Disable(this CanvasGroup canvasGroup)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0f;
        }

        public static async UniTask DisableAsync(this CanvasGroup canvasGroup, float fadeDuration)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            await canvasGroup.DOFade(0f, fadeDuration);
        }
    }
}