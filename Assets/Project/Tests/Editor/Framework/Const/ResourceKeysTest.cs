using Project.Runtime.Const;
using Project.Runtime.OutGame.View;
using NUnit.Framework;

namespace Project.Tests.Editor.Framework.Const
{
    public sealed class ResourceKeysTest
    {
        [TestCase(ExpectedResult = "pfb_ui_splash_page")]
        public string prefabs_ui_GetSplashPage()
        {
            return ResourceKeys.Prefabs.UI.GetPageKey<SplashPage>();
        }

        [TestCase("ja", ExpectedResult = "font_ja_notosans_semibold")]
        [TestCase("en", ExpectedResult = "font_en_notosans_semibold")]
        public string fonts_GetFontKey(string localeCode)
        {
            return ResourceKeys.Fonts.GetFontKey(localeCode);
        }
    }
}