using System;
using System.Linq;
using Project.Framework.Const;
using Project.Framework.Const.Editor;
using UnityEditor;
using UnityEditor.Localization;

namespace Project.Framework.Utils.Editor
{
    public static class LocalizationMenuItem
    {
        private const string FilePath = "Assets/Project/Framework/Const";

        private const string StringTableKeyItemName = "Project/Localization/StringTableのKeyの定数クラスを更新する";
        private const string StringTableKeyClassName = "LocalizationStringKeys";

        private const string AssetTableKeyItemName = "Project/Localization/AssetTableのKeyの定数クラスを更新する";
        private const string AssetTableKeyClassName = "LocalizationAssetKeys";

        [MenuItem(StringTableKeyItemName)]
        private static void GenerateStringTableKeyConst()
        {
            var collection = LocalizationEditorSettings.GetStringTableCollection(LocalizationConst.StringTableReference);
            var table = collection.GetTable(LocalizationConst.Locales.English);
            var variableDict = table.SharedData.Entries.ToDictionary(entry => entry.Key, entry => entry.Id);

            if (variableDict.Count == 0)
            {
                CustomDebug.LogError(
                    $"エントリがないため定数クラスを作成できませんでした。{Environment.NewLine} " +
                    $"Table:{LocalizationConst.StringTableReference}, Locale:{LocalizationConst.Locales.English}");
                return;
            }

            ConstClassCreator.Create(StringTableKeyClassName,
                $"LocalizationのStringTableのKeyの定数クラス" +
                $"{Environment.NewLine}\t/// 自動生成されたクラスです" +
                $"{Environment.NewLine}\t/// 手動で変更せずにEditor上部のメニューの{StringTableKeyItemName}より更新を行ってください",
                variableDict, FilePath,
                "Project.Runtime.Const");
        }

        [MenuItem(AssetTableKeyItemName)]
        private static void GenerateAssetTableKeyConst()
        {
            var collection = LocalizationEditorSettings.GetAssetTableCollection(LocalizationConst.AssetTableReference);
            var table = collection.GetTable(LocalizationConst.Locales.English);
            var variableDict = table.SharedData.Entries.ToDictionary(entry => entry.Key, entry => entry.Id);

            if (variableDict.Count == 0)
            {
                CustomDebug.LogError(
                    $"エントリがないため定数クラスを作成できませんでした。{Environment.NewLine} " +
                    $"Table:{LocalizationConst.StringTableReference}, Locale:{LocalizationConst.Locales.English}");
                return;
            }

            ConstClassCreator.Create(AssetTableKeyClassName,
                $"LocalizationのAssetTableのKeyの定数クラス" +
                $"{Environment.NewLine}\t/// 自動生成されたクラスです" +
                $"{Environment.NewLine}\t/// 手動で変更せずにEditor上部のメニューの{StringTableKeyItemName}より更新を行ってください",
                variableDict, FilePath,
                "Project.Runtime.Const");
        }
    }
}