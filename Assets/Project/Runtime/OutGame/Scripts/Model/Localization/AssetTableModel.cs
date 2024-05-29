using Project.Framework.Const;
using Project.Framework.Utils;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

namespace Project.Runtime.OutGame.Model
{
    /// <summary>
    ///     AssetTable、エントリを管理するモデル
    /// </summary>
    public static class AssetTableModel
    {
        private static AssetTable _table;

        private static AssetTable Table
        {
            get
            {
                if (_table != null)
                {
                    return _table;
                }

                CustomDebug.LogError($"[{nameof(AssetTableModel)}] Tableがロードされていません");
                return null;
            }
        }

        /// <summary>
        ///     アプリ立ち上げ時にTableをキャッシュする
        /// </summary>
        internal static void CacheTable()
        {
            _table = LocalizationSettings.AssetDatabase.GetTable(LocalizationConst.StringTableReference);
        }
    }
}