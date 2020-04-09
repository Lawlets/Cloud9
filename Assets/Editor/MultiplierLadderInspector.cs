using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BTA
{
    [CustomEditor(typeof(MultiplierLadder))]
    public class MultiplierLadderInspector : Editor
    {
        MultiplierLadder currLadder;
        string renameString;

        public override void OnInspectorGUI()
        {
            currLadder = target as MultiplierLadder;

            if (currLadder != null)
                BasicDisplay();
        }

        void BasicDisplay()
        {
            GUI.enabled = true;
            GUILayout.BeginVertical("box");
            {
                GUILayout.Label(currLadder.name, EditorStyles.boldLabel);

                // Rename Combo File
                GUILayout.BeginHorizontal("label");
                {
                    renameString = EditorGUILayout.TextField("Ladder Name", renameString, GUILayout.ExpandWidth(true));
                    //GUILayout.FlexibleSpace();
                    if (GUILayout.Button("Rename"))
                        RenameAsset(renameString);
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();
            GUILayout.Space(10f);
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            currLadder.Display();

            EditorUtility.SetDirty(currLadder);
        }

        void RenameAsset(string newName)
        {
            string assetPath = AssetDatabase.GetAssetPath(currLadder.GetInstanceID());
            AssetDatabase.RenameAsset(assetPath, newName);
            AssetDatabase.SaveAssets();
        }
    }
}

