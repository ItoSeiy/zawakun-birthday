using UnityEditor;
using UnityEngine.UI;

namespace Project.Framework.Editor
{
    // <summary>UnityのImage Editorを無効化する</summary>
    [CustomEditor(typeof(Image), true)]
    [CanEditMultipleObjects]
    public class HideImageInspector : ImageEditor
    {
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginDisabledGroup(true);
            DrawDefaultInspector();
            EditorGUI.EndDisabledGroup();
        }
    }
}