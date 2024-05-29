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
            public const string Prefix = "pfb_";

            public static class UI
            {
                private const string PageSuffix = "_page";
                private const string SheetSuffix = "_sheet";
                private const string ModalSuffix = "_modal";

                private static string PrefixInternal => $"{Prefix}ui_";

                public static string GetSheetKey<TSheet>() where TSheet : Sheet
                {
                    return $"{PrefixInternal}{GetLowerTypeName<TSheet>("Sheet")}{SheetSuffix}";
                }

                public static string GetPageKey<TPage>() where TPage : Page
                {
                    return $"{PrefixInternal}{GetLowerTypeName<TPage>("Page")}{PageSuffix}";
                }

                public static string GetModalKey<TModal>() where TModal : Modal
                {
                    return $"{PrefixInternal}{GetLowerTypeName<TModal>("Modal")}{ModalSuffix}";
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