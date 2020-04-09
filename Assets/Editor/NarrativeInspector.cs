using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BTA
{
    [CustomEditor(typeof(NarrativeBlock))]
    public class NarrativeInspector : Editor
    {
        NarrativeBlock currNarrativeBlock;

        public override void OnInspectorGUI()
        {
            currNarrativeBlock = target as NarrativeBlock;

            if (currNarrativeBlock != null)
            {
                EditorGUILayout.LabelField("Narrative Editor", EditorStyles.boldLabel);
                GUILayout.Space(30f);

                currNarrativeBlock.Display();

                EditorUtility.SetDirty(currNarrativeBlock);
            }
        }
    }
}
