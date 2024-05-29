using Project.Framework.Const;
using Project.Framework.Utils;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

namespace Project.Runtime.OutGame.Model
{
    /// <summary>
    ///     StringTable、エントリを管理するモデル
    /// </summary>
    public static class StringTableModel
    {
        private static StringTable _table;

        private static StringTable Table
        {
            get
            {
                if (_table != null)
                {
                    return _table;
                }

                CustomDebug.LogError($"[{nameof(StringTableModel)}] Tableがロードされていません");
                return null;
            }
        }

        public static string GetEntry(long id)
        {
            return Table.GetEntry(id).Value;
        }

        /// <summary>
        ///     アプリ立ち上げ時にTableをキャッシュする
        /// </summary>
        internal static void CacheTable()
        {
            LocalizationSettings.Instance.OnSelectedLocaleChanged += _ => CacheTable();

            _table = LocalizationSettings.StringDatabase.GetTable(LocalizationConst.StringTableReference);
        }
    }
}