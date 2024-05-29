using System;

namespace Project.Framework.Const.Editor
{
    public enum ScreenType
    {
        Modal = ScriptType.Modal,
        Page = ScriptType.Page,
        Sheet = ScriptType.Sheet
    }

    public enum ViewType
    {
        View = ScriptType.View,
        RecyclableView = ScriptType.RecyclableView
    }

    public enum PresenterType
    {
        ModalPresenter = ScriptType.ModalPresenter,
        PagePresenter = ScriptType.PagePresenter,
        SheetPresenter = ScriptType.SheetPresenter
    }

    public enum ScriptType
    {
        Modal,
        Page,
        Sheet,
        Model,
        View,
        RecyclableView,
        ModalPresenter,
        PagePresenter,
        SheetPresenter,
        PresenterFactory
    }

    public static class MVPScriptConst
    {
        public const string ScreenTypeName = "#SCREEN_TYPE_NAME#";
        public const string InheritedScreenName = "#INHERITED_SCREEN_NAME#";

        public const string ScreenTemplate = @"using Project.Framework.OutGame;

namespace Project.Runtime.OutGame.View
{
    public sealed class #SCREEN_TYPE_NAME# : #INHERITED_SCREEN_NAME#<#VIEW_NAME#, #VIEW_STATE_NAME#>
    {
    }
}
";

        public const string ModelName = "#MODEL_NAME#";
        public const string ViewName = "#VIEW_NAME#";
        public const string ViewStateName = "#VIEW_STATE_NAME#";
        public const string ViewClassComment = "#VIEW_CLASS_COMMENT#";
        public const string PresenterName = "#PRESENTER_NAME#";
        public const string PresenterClassComment = "#PRESENTER_CLASS_COMMENT#";
        public const string InheritedPresenterName = "#INHERITED_PRESENTER_NAME#";
        public const string PresenterFactoryName = "#PRESENTER_FACTORY_NAME#";

        public const string ModelTemplate = @"namespace Project.Runtime.OutGame.Model
{
    /// <summary>
    /// ~Model
    /// </summary>
    public sealed class #MODEL_NAME#
    {
    }
}
";

        public const string ViewTemplate = @"using Cysharp.Threading.Tasks;
using Project.Framework.OutGame;
using UnityEngine;

namespace Project.Runtime.OutGame.View
{
    /// <summary>
    /// #VIEW_CLASS_COMMENT#
    /// </summary>
    public sealed class #VIEW_NAME# : AppView<#VIEW_STATE_NAME#>
    {
        protected override UniTask<#VIEW_STATE_NAME#> Setup()
        {
            var state = new #VIEW_STATE_NAME#();
            return UniTask.FromResult(state);
        }
    }

    public sealed class #VIEW_STATE_NAME# : AppViewState
    {
    }
}
";

        public const string RecyclableAppViewTemplate = @"using Cysharp.Threading.Tasks;
using Project.Framework.OutGame;
using UnityEngine;

namespace Project.Runtime.OutGame.View
{
    /// <summary>
    /// ~RecyclableView
    /// </summary>
    public sealed class #VIEW_NAME# : RecyclableAppView<#VIEW_STATE_NAME#>
    {
        protected override UniTask<#VIEW_STATE_NAME#> Setup()
        {
            var state = new #VIEW_STATE_NAME#();
            return UniTask.FromResult(state);
        }

        protected override void Cleanup()
        {
        }
    }

    public sealed class #VIEW_STATE_NAME# : AppViewState
    {
    }
}
";

        public const string ScreenTypePresenterTemplate = @"using Cysharp.Threading.Tasks;
using Project.Runtime.OutGame.View;

namespace Project.Runtime.OutGame.Presentation
{
    /// <summary>
    /// #PRESENTER_CLASS_COMMENT#
    /// </summary>
    public sealed class #PRESENTER_NAME# : #INHERITED_PRESENTER_NAME#<#SCREEN_TYPE_NAME#, #VIEW_NAME#, #VIEW_STATE_NAME#>
    {
        public #PRESENTER_NAME#(#SCREEN_TYPE_NAME# view, ITransitionService transitionService) : base(view, transitionService)
        {
        }

        protected override UniTask ViewDidSetup(#VIEW_STATE_NAME# state)
        {
            return UniTask.FromResult(state);
        }
    }
}
";

        public const string PresenterFactoryTemplate = @"using Project.Runtime.OutGame.View;

namespace Project.Runtime.OutGame.Presentation
{
    public sealed class #PRESENTER_FACTORY_NAME#
    {
        public #PRESENTER_NAME# Create(#SCREEN_TYPE_NAME# view, ITransitionService transitionService)
        {
            return new #PRESENTER_NAME#(view, transitionService);
        }
    }
}";

        public static PresenterType ConvertScreenTypeToPresenterType(ScreenType screenType)
        {
            return screenType switch
            {
                ScreenType.Modal => PresenterType.ModalPresenter,
                ScreenType.Page => PresenterType.PagePresenter,
                ScreenType.Sheet => PresenterType.SheetPresenter,
                _ => throw new ArgumentOutOfRangeException(nameof(screenType), screenType, null)
            };
        }

        public static ScreenType ConvertPresenterTypeToScreenType(PresenterType presenterType)
        {
            return presenterType switch
            {
                PresenterType.ModalPresenter => ScreenType.Modal,
                PresenterType.PagePresenter => ScreenType.Page,
                PresenterType.SheetPresenter => ScreenType.Sheet,
                _ => throw new ArgumentOutOfRangeException(nameof(presenterType), presenterType, null)
            };
        }

        public static string GetScriptFolderPath(ScriptType scriptType, string folderName)
        {
            switch (scriptType)
            {
                case ScriptType.Modal:
                case ScriptType.Page:
                case ScriptType.Sheet:
                case ScriptType.View:
                case ScriptType.RecyclableView:
                    return $"Assets/Project/Runtime/OutGame/Scripts/View/{folderName}";
                case ScriptType.Model:
                    return $"Assets/Project/Runtime/OutGame/Scripts/Model/{folderName}";
                case ScriptType.ModalPresenter:
                case ScriptType.PagePresenter:
                case ScriptType.SheetPresenter:
                case ScriptType.PresenterFactory:
                    return $"Assets/Project/Runtime/OutGame/Scripts/Presenter/{folderName}";
                default:
                    throw new ArgumentOutOfRangeException(nameof(scriptType), scriptType, null);
            }
        }
    }
}