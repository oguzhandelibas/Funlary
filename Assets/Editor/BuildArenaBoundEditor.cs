using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Funlary
{
    [CustomEditor(typeof(GroundBounds))]
    public class BuildArenaBoundEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GroundBounds groundBounds = (GroundBounds)target;

            if (GUILayout.Button("Set Arena Bound"))
            {
                groundBounds.SetArenaBound();
            }

            if (GUILayout.Button("Delete Arena Bound"))
            {
                groundBounds.DeleteArenaBound();
            }
        }
    }
}
