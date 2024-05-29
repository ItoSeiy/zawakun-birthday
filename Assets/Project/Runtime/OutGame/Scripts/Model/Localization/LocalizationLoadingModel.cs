using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Framework.Utils;
using R3;
using UnityEngine.Localization.Settings;

namespace Project.Runtime.OutGame.Model
{
    /// <summary>
    ///     Localization全体のロード状況を管理するモデル
    /// </summary>
    public class LocalizationLoadingModel
    {
        private readonly ReactiveProperty<float> _loadingProgress = new();

        public ReadOnlyReactiveProperty<float> LoadingProgress => _loadingProgress;

        /// <summary>
        ///     アプリ立ち上げ時のLocalizationのロードの進捗の更新を開始する
        /// </summary>
        public async UniTask StartUpdateLoadingProgress(CancellationToken ct)
        {
            // Localizationの初期化処理はアプリを立ち上げた時や、選択中のLocaleが変わった時に自動的に開始される
            // ロード対象は選択されているローケールのStringTable、AssetTableでPreloadにチェックが付いているもの
            while (LocalizationSettings.InitializationOperation.IsDone == false)
            {
                await UniTask.Yield(ct);
                CustomDebug.Log(
                    $"[Localization] Loading {LocalizationSettings.InitializationOperation.PercentComplete * 100}%");
                _loadingProgress.Value = LocalizationSettings.InitializationOperation.PercentComplete;
            }
        }

        /// <summary>
        ///     LocalizationのStringTableとAssetTableをキャッシュする
        /// </summary>
        public void CacheLocalizationTables()
        {
            if (LocalizationSettings.InitializationOperation.IsDone)
            {
                StringTableModel.CacheTable();
                AssetTableModel.CacheTable();
            }
            else
            {
                CustomDebug.LogError("[Localization] テーブルの初期ロードを完了しない状態でキャッシュしようとした為、キャッシュできませんでした。");
            }
        }
    }
}