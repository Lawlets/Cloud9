using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BTA
{
    [CustomEditor(typeof(AttackStruct))]
    public class AttackStructEditor : Editor
    {
        AttackStruct currAttackStruct;
        string renameString;

        public override void OnInspectorGUI()
        {
            currAttackStruct = target as AttackStruct;

            //base.OnInspectorGUI();

            if (currAttackStruct != null)
                BasicDisplay();
        }

        void BasicDisplay()
        {
            GUI.enabled = true;
            GUILayout.BeginVertical("box");
            {
                GUILayout.Label(currAttackStruct.name, EditorStyles.boldLabel);

                // Rename Combo File
                GUILayout.BeginHorizontal("label");
                {
                    renameString = EditorGUILayout.TextField("Attack Name", renameString, GUILayout.ExpandWidth(true));
                    //GUILayout.FlexibleSpace();
                    if (GUILayout.Button("Rename"))
                        RenameAsset(renameString);
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();
            GUILayout.Space(10f);
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            currAttackStruct.Display();

            EditorUtility.SetDirty(currAttackStruct);
        }

        void RenameAsset(string newName)
        {
            string assetPath = AssetDatabase.GetAssetPath(currAttackStruct.GetInstanceID());
            AssetDatabase.RenameAsset(assetPath, newName);
            AssetDatabase.SaveAssets();
        }
    }
}

