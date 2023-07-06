using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SnapDropZone))]
public class SnapDropZoneEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        SnapDropZone snapDropZone = target as SnapDropZone;
        EditorUtility.SetDirty(snapDropZone);
        
        EditorGUI.BeginDisabledGroup(!snapDropZone.identifyObjectOverTag);
        snapDropZone.selectedTag = EditorGUI.TagField(
            new Rect(18, 120, 200, 20),
            "",
            snapDropZone.selectedTag);
        EditorGUI.EndDisabledGroup();

        if (GUILayout.Button("Reset"))
        {
            snapDropZone.snapDropObject = null;
            snapDropZone.grabableAfterSnap = false;
            snapDropZone.scaleAfterSnap = false;
            snapDropZone.isSnapable = false;
        }
    }
}
