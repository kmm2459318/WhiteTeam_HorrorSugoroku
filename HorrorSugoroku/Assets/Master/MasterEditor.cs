#if UNITY_EDITOR
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Master_Item))]
public class MasterItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("�f�[�^�X�V"))
        {
            ((Master_Item)target).LoadItemData();
        }
    }
}

[CustomEditor(typeof(Master_Event))]
public class MasterEventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("�f�[�^�X�V"))
        {
            ((Master_Event)target).LoadEventData();
        }
    }
}

[CustomEditor(typeof(Master_Debuff))]
public class MasterDebuffEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("�f�[�^�X�V"))
        {
            ((Master_Debuff)target).LoadDebuffData();
        }
    }
}
#endif
