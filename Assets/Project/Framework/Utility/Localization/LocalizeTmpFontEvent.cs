using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

namespace Project.Framework.Utils
{
    /// <summary>
    ///     TmpFontAsset 用の LocalizedAssetEvent
    /// </summary>
    [AddComponentMenu("Localization/Asset/" + nameof(LocalizeTmpFontEvent))]
    public class LocalizeTmpFontEvent : LocalizedAssetEvent<TMP_FontAsset, LocalizedTmpFont, UnityEventTmpFont>
    {
    }

    /// <summary>
    ///     TmpFontAsset を引数とする Unity Event
    /// </summary>
    [Serializable]
    public class UnityEventTmpFont : UnityEvent<TMP_FontAsset>
    {
    }
}