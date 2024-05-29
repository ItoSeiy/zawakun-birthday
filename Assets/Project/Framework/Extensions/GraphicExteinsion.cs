using Project.Framework.Utils;
using UnityEngine.UI;
using uPalette.Runtime.Core;

namespace Project.Framework.Extensions
{
    /// <summary>Graphicの拡張</summary>
    public static class GraphicExtensions
    {
        /// <summary>uPaletteのEntryIdから色を設定する</summary>
        public static void SetColorFromEntryId(this Graphic graphic, ColorEntryId colorEntryId)
        {
            if (PaletteStore.Instance == null)
            {
                CustomDebug.LogError($"Project内に{nameof(PaletteStore.Instance.ColorPalette)}が存在しないので色を設定できません");
                return;
            }

            // 色を設定する
            if (PaletteStore.Instance.ColorPalette.TryGetActiveValue(colorEntryId.Value, out var colorProperty))
            {
                graphic.color = colorProperty.Value;
            }
        }
    }
}