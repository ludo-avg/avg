using UnityEngine;
using TMPro;
using Modules;

#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(DialogueTypeWriter))]
class TypewriterEditor : UnityEditor.Editor
{
    private DialogueTypeWriter Target => (DialogueTypeWriter)target;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        UnityEditor.EditorGUILayout.Space();
        UnityEditor.EditorGUI.BeginDisabledGroup(!Application.isPlaying || !Target.isActiveAndEnabled);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Restart"))
        {
            Target.OutputText(Target.GetComponent<TMP_Text>().text);
        }
        if (GUILayout.Button("Complete"))
        {
            Target.CompleteOutput();
        }
        GUILayout.EndHorizontal();
        UnityEditor.EditorGUI.EndDisabledGroup();
    }
}
#endif
