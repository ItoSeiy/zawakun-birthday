using UnityScreenNavigator.Runtime.Core.Modal;
using UnityScreenNavigator.Runtime.Core.Page;
using UnityScreenNavigator.Runtime.Core.Sheet;

namespace Project.Runtime.Const
{
    public static class ResourceKeys
    {
        private static string GetLowerTypeName<T>(string exclude = "")
        {
            return $"{typeof(T).Name.Replace(exclude, string.Empty).ToLower()}";
        }

        public static class Prefabs
        {
            public const string PrefabsPrefix = "pfb_";

            public static class UI
            {
                public const string PageSuffix = "_page";
                public const string SheetSuffix = "_sheet";
                public const string ModalSuffix = "_modal";

                public static string UIPrefix => $"{PrefabsPrefix}ui_";

                public static string GetSheetKey<TSheet>() where TSheet : Sheet
                {
                    return $"{UIPrefix}{GetLowerTypeName<TSheet>("Sheet")}{SheetSuffix}";
                }

                public static string GetPageKey<TPage>() where TPage : Page
                {
                    return $"{UIPrefix}{GetLowerTypeName<TPage>("Page")}{PageSuffix}";
                }

                public static string GetModalKey<TModal>() where TModal : Modal
                {
                    return $"{UIPrefix}{GetLowerTypeName<TModal>("Modal")}{ModalSuffix}";
                }
            }
        }

        public static class Textures
        {
            public const string Prefix = "tex_";

            public static class Icon
            {
                private const string IconSuffix = "_icon";

                public static string GetKey(string iconName)
                {
                    return $"{Prefix}{iconName}{IconSuffix}";
                }
            }
        }

        public static class Fonts
        {
            public const string Prefix = "font_";
            private const string DefaultFontNameSuffix = "_notosans_semibold";

            public static string GetFontKey(string localeCode)
            {
                return $"{Prefix}{localeCode}{DefaultFontNameSuffix}";
            }
        }
    }
}