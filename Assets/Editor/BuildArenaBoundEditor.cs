using UnityEditor;
using UnityEngine;

namespace Funlary
{
    [CustomEditor(typeof(Arena))]
    public class BuildArenaBoundEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            Arena arena = (Arena)target;

            if (GUILayout.Button("Set Arena Bound"))
            {
                arena.SetArenaBound();
            }

            if (GUILayout.Button("Delete Arena Bound"))
            {
                arena.DeleteArenaBound();
            }

            EditorUtility.SetDirty(arena);
        }
    }
}
