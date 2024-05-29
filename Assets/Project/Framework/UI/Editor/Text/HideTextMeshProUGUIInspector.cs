using TMPro;
using TMPro.EditorUtilities;
using UnityEditor;

namespace Project.Framework.Editor
{
    [CustomEditor(typeof(TextMeshProUGUI), true)]
    [CanEditMultipleObjects]
    public class HideTextMeshProUGUIInspector : TMP_EditorPanelUI
    {
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginDisabledGroup(true);
            base.OnInspectorGUI();
            EditorGUI.EndDisabledGroup();
        }
    }
}