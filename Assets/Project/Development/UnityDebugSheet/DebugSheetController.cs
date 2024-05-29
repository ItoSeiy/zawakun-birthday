using Project.Framework.OutGame;
using IngameDebugConsole;
using Tayx.Graphy;
using UnityDebugSheet.Runtime.Core.Scripts;
using UnityDebugSheet.Runtime.Extensions.Graphy;
using UnityDebugSheet.Runtime.Extensions.IngameDebugConsole;
using UnityDebugSheet.Runtime.Extensions.Unity;
using UnityEngine;

namespace Project.Development
{
    public class DebugSheetController : MonoBehaviour
    {
        private void Start()
        {
            var rootPage = DebugSheet.Instance.GetOrCreateInitialPage();

            rootPage.AddPageLinkButton<SettingDebugPage>(nameof(SettingDebugPage));

            rootPage.AddPageLinkButton<IngameDebugConsoleDebugPage>("In-Game Debug Console",
                onLoad: x => x.page.Setup(DebugLogManager.Instance));

            rootPage.AddPageLinkButton<GraphyDebugPage>(nameof(Graphy),
                onLoad: x => x.page.Setup(GraphyManager.Instance));

            rootPage.AddPageLinkButton<SystemInfoDebugPage>(nameof(SystemInfo));
            rootPage.AddPageLinkButton<ApplicationDebugPage>(nameof(Application));
            rootPage.AddPageLinkButton<TimeDebugPage>(nameof(Time));
            rootPage.AddPageLinkButton<QualitySettingsDebugPage>(nameof(QualitySettings));
            rootPage.AddPageLinkButton<ScreenDebugPage>(nameof(Screen));
            rootPage.AddPageLinkButton<InputDebugPage>(nameof(Input));
            rootPage.AddPageLinkButton<GraphicsDebugPage>(nameof(Graphics));
        }
    }
}