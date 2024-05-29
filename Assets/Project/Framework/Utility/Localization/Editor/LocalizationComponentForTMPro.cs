using System;
using TMPro;
using UnityEditor;
using UnityEditor.Events;
using UnityEngine.Events;
using UnityEngine.Localization.Components;
using Project.Framework.Utils;

namespace Project.Framework.Editor
{
    public static class LocalizationComponentForTMPro
    {
        [MenuItem("CONTEXT/TextMeshProUGUI/Localize Extension")]
        private static void LocalizeTMProTextWithFontAssets(MenuCommand command)
        {
            var target = command.context as TextMeshProUGUI;
            SetupForLocalizeString(target);
            SetupForLocalizeTmpFont(target);
        }

        /// <summary>
        ///     LocalizeStringEvent コンポーネントをアタッチすると同時に自動的に UpdateAsset イベントに text プロパティを変更する処理を追加する
        /// </summary>
        /// <param name="target">TextMeshProUGUI</param>
        private static void SetupForLocalizeString(TextMeshProUGUI target)
        {
            var comp = Undo.AddComponent(target.gameObject, typeof(LocalizeStringEvent)) as LocalizeStringEvent;
            var setStringMethod = target.GetType().GetProperty("text")?.GetSetMethod();

            if (setStringMethod != null)
            {
                var methodDelegate =
                    Delegate.CreateDelegate(typeof(UnityAction<string>), target,
                        setStringMethod) as UnityAction<string>;
                if (comp != null)
                {
                    UnityEventTools.AddPersistentListener(comp.OnUpdateString, methodDelegate);
                    comp.OnUpdateString.SetPersistentListenerState(0, UnityEventCallState.EditorAndRuntime);
                }
            }
        }

        /// <summary>
        ///     LocalizeTmpFontEvent コンポーネントをアタッチすると同時に自動的に UpdateAsset イベントに font プロパティを変更する処理を追加する
        /// </summary>
        /// <param name="target">TextMeshProUGUI</param>
        private static void SetupForLocalizeTmpFont(TextMeshProUGUI target)
        {
            var comp =
                Undo.AddComponent(target.gameObject, typeof(LocalizeTmpFontEvent)) as LocalizeTmpFontEvent;

            var setStringMethod = target.GetType().GetProperty("font")?.GetSetMethod();
            if (setStringMethod != null)
            {
                var methodDelegate =
                    Delegate.CreateDelegate(typeof(UnityAction<TMP_FontAsset>), target, setStringMethod) as
                        UnityAction<TMP_FontAsset>;

                if (comp != null)
                {
                    UnityEventTools.AddPersistentListener(comp.OnUpdateAsset, methodDelegate);
                }
            }

            if (comp != null)
            {
                comp.OnUpdateAsset.SetPersistentListenerState(0, UnityEventCallState.EditorAndRuntime);
            }
        }
    }
}