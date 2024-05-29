using System;
using System.Linq;
using System.Threading.Tasks;
using UnityDebugSheet.Runtime.Core.Scripts;
using UnityEngine;
using UnityEngine.Localization.Settings;
using uPalette.Generated;
using uPalette.Runtime.Core;

namespace Project.Framework.OutGame
{
    public sealed class SettingDebugPage : DefaultDebugPageBase
    {
        protected override string Title { get; } = "Setting";

        public override async Task Initialize()
        {
            await LocalizationSettings.InitializationOperation.Task;
            UPaletteTheme();
            Locale();
            PlayerPrefsInternal();
        }

        private void UPaletteTheme()
        {
            var colorPalette = PaletteStore.Instance.ColorPalette;
            var activeTheme = ColorTheme.DarkMode;

            foreach (ColorTheme colorTheme in Enum.GetValues(typeof(ColorTheme)))
            {
                if (colorPalette.ActiveTheme.Value.Id == colorTheme.ToThemeId())
                {
                    activeTheme = colorTheme;
                }
            }

            AddEnumPicker(activeTheme, "uPalette Theme",
                activeValueChanged: x =>
                {
                    var theme = Enum.Parse<ColorTheme>(x.ToString());
                    colorPalette.SetActiveTheme(theme.ToThemeId());
                });
        }

        private void Locale()
        {
            var availableLocales = LocalizationSettings.AvailableLocales.Locales;
            var localesCodeStr = availableLocales.Select(x => x.Identifier.Code).ToArray();
            var localeDisplayName = availableLocales.Select(x => x.ToString()).ToArray();

            AddPicker(localeDisplayName, GetActiveOptionIndex(), "Languages",
                activeOptionChanged: x => LocalizationSettings.SelectedLocale = availableLocales[x]);

            int GetActiveOptionIndex()
            {
                var selectedLocale = LocalizationSettings.Instance.GetSelectedLocale();

                var activeOptionIndex = 0;
                for (; activeOptionIndex < availableLocales.Count; activeOptionIndex++)
                {
                    if (selectedLocale.Identifier.Code == localesCodeStr[activeOptionIndex])
                    {
                        return activeOptionIndex;
                    }
                }

                return -1;
            }
        }

        private void PlayerPrefsInternal()
        {
            AddButton("Delete All PlayerPrefs", clicked: PlayerPrefs.DeleteAll);
        }
    }
}