using System.Threading;
using Cysharp.Threading.Tasks;
using UnityScreenNavigator.Runtime.Core.Sheet;

namespace Project.Runtime.OutGame.Composition
{
    public sealed partial class TransitionService
    {
        private const string LoadingUIKey = "pfb_ui_loading";
        private const string LoadingIconUIKey = "pfb_ui_loading_icon";

        private string _loadingIconUIId;
        private string _loadingUIId;

        private static SheetContainer LoadingUIContainer => SheetContainer.Find("LoadingUIContainer");

        public async UniTask RegisterLoadLoadingUIs(CancellationToken ct)
        {
            await (
                LoadingUIContainer.Register(LoadingUIKey, x => _loadingUIId = x.sheetId)
                    .ToUniTask(cancellationToken: ct),
                LoadingUIContainer.Register(LoadingIconUIKey, x => _loadingIconUIId = x.sheetId)
                    .ToUniTask(cancellationToken: ct));
        }

        /// <summary>
        ///     画面全体が触れなくなるローディング画面を表示
        /// </summary>
        public UniTask ShowLoadingUI(CancellationToken ct)
        {
            return LoadingUIContainer.Show(_loadingUIId, false).ToUniTask(cancellationToken: ct);
        }

        /// <summary>
        ///     ローディングアイコンを表示
        /// </summary>
        public UniTask ShowLoadingIconUI(CancellationToken ct)
        {
            return　LoadingUIContainer.Show(_loadingIconUIId, false).ToUniTask(cancellationToken: ct);
        }

        /// <summary>
        ///     今表示されているロード中表示のアイコンを非表示にする
        /// </summary>
        public UniTask HideLoadingUI(CancellationToken ct)
        {
            return LoadingUIContainer.ActiveSheet == null
                ? UniTask.CompletedTask
                : LoadingUIContainer.Hide(false).ToUniTask(cancellationToken: ct);
        }
    }
}