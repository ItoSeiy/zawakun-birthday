using Project.Framework.Const;
using Project.Framework.OutGame;
using TMPro;
using UnityEngine;

namespace Project.Framework.Utils
{
    /// <summary> テキストがはみ出した時の状態を設定するクラス </summary>
    public class TextOverflowUtility
    {
        /// <summary>フォントサイズの更新</summary>
        public static void UpdateFontSize(TextOverflowType overflowType, TextMeshProUGUI textComponent)
        {
            switch (overflowType)
            {
                case TextOverflowType.Expand:
                    textComponent.enableAutoSizing = false;
                    break;
                case TextOverflowType.Shrink:
                    textComponent.enableAutoSizing = true;
                    break;
                case TextOverflowType.Marquee:
                    textComponent.enableAutoSizing = false;
                    Debug.LogError($"{nameof(TextOverflowType.Marquee)}はまだ未実装です");
                    break;
            }
        }

        /// <summary>テキストエリアの更新</summary>
        public static void UpdateTextArea(BaseText baseTextComponent)
        {
            switch (baseTextComponent.TextOverflowType)
            {
                case TextOverflowType.Shrink:
                    // 何もしない
                    break;
                case TextOverflowType.Expand:
                    baseTextComponent.FitTextBoxArea();
                    break;
                case TextOverflowType.Marquee:
                    Debug.LogError($"{nameof(TextOverflowType.Marquee)}はまだ未実装です");
                    break;
            }
        }
    }
}