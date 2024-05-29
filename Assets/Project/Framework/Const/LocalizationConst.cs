using UnityEngine.Localization.Tables;

namespace Project.Framework.Const
{
    public static class LocalizationConst
    {
        public static readonly TableReference StringTableReference = "ProjectText";
        public static readonly TableReference AssetTableReference = "ProjectAsset";

        public static class Locales
        {
            public const string ChineseSimplified = "zh-hans";
            public const string ChineseTraditional = "zh-hant";
            public const string English = "en";
            public const string French = "fr";
            public const string German = "de";
            public const string Italian = "it";
            public const string Japanese = "ja";
            public const string Korean = "ko";
            public const string Portuguese = "pt";
            public const string Spanish = "es";
        }
    }
}