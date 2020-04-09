using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BTA
{
    [CustomEditor(typeof(ComboAttackStruct))]
    public class ComboPieceInspector : Editor
    {
        ComboAttackStruct currComboPiece;
        string renameString;

        public override void OnInspectorGUI()
        {
            currComboPiece = target as ComboAttackStruct;

            if (currComboPiece != null)
                BasicDisplay();
        }

        void BasicDisplay()
        {
            GUI.enabled = true;
            GUILayout.BeginVertical("box");
            {
                GUILayout.Label(currComboPiece.name, EditorStyles.boldLabel);

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

            currComboPiece.Display();

            EditorUtility.SetDirty(currComboPiece);
        }

        void RenameAsset(string newName)
        {
            string assetPath = AssetDatabase.GetAssetPath(currComboPiece.GetInstanceID());
            AssetDatabase.RenameAsset(assetPath, newName);
            AssetDatabase.SaveAssets();
        }
    }
}
