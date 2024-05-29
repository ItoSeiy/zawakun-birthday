using System;

namespace uPalette.Generated
{
public enum ColorTheme
    {
        DarkMode,
        LightMode,
    }

    public static class ColorThemeExtensions
    {
        public static string ToThemeId(this ColorTheme theme)
        {
            switch (theme)
            {
                case ColorTheme.DarkMode:
                    return "f8cf3d9b-4dd2-448f-96a8-c9e4dc1f7f1e";
                case ColorTheme.LightMode:
                    return "1cdbd549-64f1-4357-aeea-5c3da862cbfc";
                default:
                    throw new ArgumentOutOfRangeException(nameof(theme), theme, null);
            }
        }
    }

    public enum ColorEntry
    {
        Black_Design,
        Black_Alpha,
        White_Design,
        Transparent,
        White,
        Black,
    }

    public static class ColorEntryExtensions
    {
        public static string ToEntryId(this ColorEntry entry)
        {
            switch (entry)
            {
                case ColorEntry.Black_Design:
                    return "3ef5daa9-1188-4f97-a933-5f5c97e8e4b2";
                case ColorEntry.Black_Alpha:
                    return "218ce5ae-d00c-4d61-93cd-f3573ad247db";
                case ColorEntry.White_Design:
                    return "be0a87e7-0486-4de9-824f-110597d3208a";
                case ColorEntry.Transparent:
                    return "8a61abe8-56ab-4bb3-b33c-e5f261c7a54b";
                case ColorEntry.White:
                    return "a7ede8a4-1d37-4ee4-8504-1cd19d5ea188";
                case ColorEntry.Black:
                    return "12f4b4b5-9941-4634-8ecd-828bbe9dd926";
                default:
                    throw new ArgumentOutOfRangeException(nameof(entry), entry, null);
            }
        }
    }

    public enum GradientTheme
    {
        Default,
    }

    public static class GradientThemeExtensions
    {
        public static string ToThemeId(this GradientTheme theme)
        {
            switch (theme)
            {
                case GradientTheme.Default:
                    return "edcb03fd-c349-4fb4-b608-62e672819f40";
                default:
                    throw new ArgumentOutOfRangeException(nameof(theme), theme, null);
            }
        }
    }

    public enum GradientEntry
    {
    }

    public static class GradientEntryExtensions
    {
        public static string ToEntryId(this GradientEntry entry)
        {
            switch (entry)
            {
                default:
                    throw new ArgumentOutOfRangeException(nameof(entry), entry, null);
            }
        }
    }

    public enum CharacterStyleTheme
    {
        Default,
    }

    public static class CharacterStyleThemeExtensions
    {
        public static string ToThemeId(this CharacterStyleTheme theme)
        {
            switch (theme)
            {
                case CharacterStyleTheme.Default:
                    return "e476c0c0-b41d-4fc5-84c4-406fd4880548";
                default:
                    throw new ArgumentOutOfRangeException(nameof(theme), theme, null);
            }
        }
    }

    public enum CharacterStyleEntry
    {
    }

    public static class CharacterStyleEntryExtensions
    {
        public static string ToEntryId(this CharacterStyleEntry entry)
        {
            switch (entry)
            {
                default:
                    throw new ArgumentOutOfRangeException(nameof(entry), entry, null);
            }
        }
    }

    public enum CharacterStyleTMPTheme
    {
        DarkMode,
        LightMode,
    }

    public static class CharacterStyleTMPThemeExtensions
    {
        public static string ToThemeId(this CharacterStyleTMPTheme theme)
        {
            switch (theme)
            {
                case CharacterStyleTMPTheme.DarkMode:
                    return "2e099965-66d6-4b98-8245-679e2bcbb497";
                case CharacterStyleTMPTheme.LightMode:
                    return "32dccb09-5607-4990-b037-0cdace334470";
                default:
                    throw new ArgumentOutOfRangeException(nameof(theme), theme, null);
            }
        }
    }

    public enum CharacterStyleTMPEntry
    {
    }

    public static class CharacterStyleTMPEntryExtensions
    {
        public static string ToEntryId(this CharacterStyleTMPEntry entry)
        {
            switch (entry)
            {
                default:
                    throw new ArgumentOutOfRangeException(nameof(entry), entry, null);
            }
        }
    }
}
