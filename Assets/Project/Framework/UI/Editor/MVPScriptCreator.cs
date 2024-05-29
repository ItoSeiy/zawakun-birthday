using System;
using System.IO;
using Project.Framework.Const.Editor;
using UnityEditor;
using UnityEngine;

namespace Project.Framework.Editor
{
    public class MVPScriptCreator : EditorWindow
    {
        private string _folderName = "Shared";
        private string _japanseScreenNameComment = "タイトル";

        private bool _createModel = false;
        private string _modelClassName = "Model";

        private ViewType _viewType = ViewType.View;
        private bool _createView = true;
        private string _viewClassName = "View";

        private ScreenType _screenType = ScreenType.Modal;
        private string _screenClassName = "Modal Page Sheet";

        private PresenterType _presenterType = PresenterType.SheetPresenter;
        private bool _createPresenter = true;
        private string _presenterClassName = "Presenter";

        private void OnGUI()
        {
            _folderName = EditorGUILayout.TextField(ObjectNames.NicifyVariableName(nameof(_folderName)), _folderName);

            EditorGUILayout.Space();

            _japanseScreenNameComment = EditorGUILayout.TextField("日本語での画面名", _japanseScreenNameComment);
            EditorGUILayout.HelpBox($"ViewとPresenterのコメントに使用されます{Environment.NewLine}" +
                                    $"出力結果) {_japanseScreenNameComment}のView{Environment.NewLine}" +
                                    $"　　　　  {_japanseScreenNameComment}のPresenter", MessageType.Info);

            EditorGUILayout.Space(10f);

            _createModel = EditorGUILayout.Toggle(ObjectNames.NicifyVariableName(nameof(_createModel)), _createModel);

            if (_createModel)
            {
                _modelClassName = EditorGUILayout.TextField(ObjectNames.NicifyVariableName(nameof(_modelClassName)), _modelClassName);
            }

            EditorGUILayout.Space(10f);

            _createView = EditorGUILayout.Toggle(ObjectNames.NicifyVariableName(nameof(_createView)), _createView);

            if (_createView)
            {
                SetViewField(ObjectNames.NicifyVariableName(nameof(_viewClassName)));
                SetScreenField(ObjectNames.NicifyVariableName(nameof(_screenType)), "Modal Class Name", "Page Class Name", "Sheet Class Name");
            }

            EditorGUILayout.Space(10f);

            _createPresenter = EditorGUILayout.Toggle(ObjectNames.NicifyVariableName(nameof(_createPresenter)), _createPresenter);

            if (_createPresenter)
            {
                if (_createView)
                {
                    using (new EditorGUI.DisabledScope(true))
                    {
                        _presenterType = MVPScriptConst.ConvertScreenTypeToPresenterType(_screenType);
                        _presenterType = (PresenterType)EditorGUILayout.EnumPopup(ObjectNames.NicifyVariableName(nameof(_presenterType)), _presenterType);
                    }
                    _presenterClassName = EditorGUILayout.TextField(ObjectNames.NicifyVariableName(nameof(_presenterClassName)), _presenterClassName);
                    EditorGUILayout.Space();
                }
                else
                {
                    _presenterType = (PresenterType)EditorGUILayout.EnumPopup(ObjectNames.NicifyVariableName(nameof(_presenterType)), _presenterType);
                    _presenterClassName = EditorGUILayout.TextField(ObjectNames.NicifyVariableName(nameof(_presenterClassName)), _presenterClassName);

                    SetViewField("Inherited View Class Name");
                    SetScreenField("Inherited Screen Type", "Inherited Modal Class Name",
                        "Inherited Page Class Name", " Inherited Sheet Class Name", false);
                }
            }

            EditorGUILayout.Space(10f);

            if (GUILayout.Button("Create"))
            {
                if (_createModel)
                {
                    CreateScript(_modelClassName, ScriptType.Model);
                }

                if (_createView)
                {
                    CreateScript(_viewClassName, (ScriptType)_viewType);
                    CreateScript(_screenClassName, (ScriptType)_screenType);
                }

                if (_createPresenter)
                {
                    CreateScript(_presenterClassName, (ScriptType)_presenterType);
                    CreateScript($"{_presenterClassName}Factory", ScriptType.PresenterFactory);
                }
                AssetDatabase.Refresh();
            }
        }

        private void SetViewField(string viewFieldName)
        {
            EditorGUILayout.Space();

            _viewType = (ViewType)EditorGUILayout.EnumPopup(ObjectNames.NicifyVariableName(nameof(_viewType)), _viewType);

            _viewClassName = EditorGUILayout.TextField(viewFieldName, _viewClassName);
        }

        private void SetScreenField(string viewTypeStringName, string modalFieldName, string pageFieldName, string sheetFieldName, bool enumFieldActive = true)
        {
            EditorGUILayout.Space();

            if (enumFieldActive == false)
            {
                _screenType = MVPScriptConst.ConvertPresenterTypeToScreenType(_presenterType);
            }

            using (new EditorGUI.DisabledScope(!enumFieldActive))
            {
                _screenType = (ScreenType)EditorGUILayout.EnumPopup(viewTypeStringName, _screenType);
            }

            _screenClassName = _screenType switch
            {
                ScreenType.Modal => EditorGUILayout.TextField(modalFieldName, _screenClassName),
                ScreenType.Page => EditorGUILayout.TextField(pageFieldName, _screenClassName),
                ScreenType.Sheet => EditorGUILayout.TextField(sheetFieldName, _screenClassName),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private void CreateScript(string createClassName, ScriptType scriptType)
        {
            createClassName = createClassName.Replace(" ", "");

            var filePath = Path.Combine(MVPScriptConst.GetScriptFolderPath(scriptType, _folderName), $"{createClassName}.cs");
            // フォルダを作成
            var folderPath = Path.GetDirectoryName(filePath);
            CreateFolder(folderPath);

            // 重複しないアセットのパスを取得
            var assetPath = AssetDatabase.GenerateUniqueAssetPath(filePath);

            string code;
            switch (scriptType)
            {
                case ScriptType.Modal:
                case ScriptType.Page:
                case ScriptType.Sheet:
                    code = MVPScriptConst.ScreenTemplate
                        .Replace(MVPScriptConst.ScreenTypeName, createClassName)
                        .Replace(MVPScriptConst.InheritedScreenName, scriptType.ToString())
                        .Replace(MVPScriptConst.ViewName, _viewClassName)
                        .Replace(MVPScriptConst.ViewStateName, $"{_viewClassName}State");
                    break;
                case ScriptType.Model:
                    code = MVPScriptConst.ModelTemplate
                        .Replace(MVPScriptConst.ModelName, createClassName);
                    break;
                case ScriptType.View:
                    code = MVPScriptConst.ViewTemplate
                        .Replace(MVPScriptConst.ViewName, createClassName)
                        .Replace(MVPScriptConst.ViewStateName, $"{createClassName}State")
                        .Replace(MVPScriptConst.ViewClassComment, $"{_japanseScreenNameComment}のView");
                    break;  
                case ScriptType.RecyclableView:
                    code = MVPScriptConst.RecyclableAppViewTemplate
                        .Replace(MVPScriptConst.ViewName, createClassName)
                        .Replace(MVPScriptConst.ViewStateName, $"{createClassName}State");
                    break;
                case ScriptType.ModalPresenter:
                case ScriptType.PagePresenter:
                case ScriptType.SheetPresenter:
                    code = MVPScriptConst.ScreenTypePresenterTemplate
                        .Replace(MVPScriptConst.PresenterName, createClassName)
                        .Replace(MVPScriptConst.InheritedPresenterName, $"{scriptType}Base")
                        .Replace(MVPScriptConst.ScreenTypeName, _screenClassName)
                        .Replace(MVPScriptConst.ViewName,   _viewClassName)
                        .Replace(MVPScriptConst.ViewStateName, $"{_viewClassName}State")
                        .Replace(MVPScriptConst.PresenterClassComment, $"{_japanseScreenNameComment}のPresenter");
                    break;
                case ScriptType.PresenterFactory:
                    code = MVPScriptConst.PresenterFactoryTemplate
                        .Replace(MVPScriptConst.PresenterFactoryName, createClassName)
                        .Replace(MVPScriptConst.ScreenTypeName, _screenClassName)
                        .Replace(MVPScriptConst.PresenterName, _presenterClassName);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(scriptType), scriptType, null);
            }
            File.WriteAllText(assetPath, code);
        }

        /// <summary>
        /// 指定されたパスのフォルダを生成する
        /// </summary>
        /// <param name="path">フォルダパス（例: Assets/Sample/FolderName）</param>
        private static void CreateFolder(string path)
        {
            var target = "";
            var splitChars = new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar };
            foreach (var dir in path.Split(splitChars))
            {
                var parent = target;
                target = Path.Combine(target, dir);
                if (!AssetDatabase.IsValidFolder(target))
                {
                    AssetDatabase.CreateFolder(parent, dir);
                }
            }
        }

        [MenuItem("Project/MVP/ファイル作成")]
        private static void Open()
        {
            GetWindow<MVPScriptCreator>($"{nameof(MVPScriptCreator)}");
        }
    }
}