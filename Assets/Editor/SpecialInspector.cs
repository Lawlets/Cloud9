using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BTA
{

    [CustomEditor(typeof(SpecialAttack))]
    public class SpecialInspector : Editor
    {
        SpecialAttack currSpecial;
        string RenameString = "";

        int currLevelID = 0;

        public override void OnInspectorGUI()
        {
            currSpecial = target as SpecialAttack;

            // *** MANAGE / DISPLAY ***
            if (currSpecial.SpecialLevels.Count == 0)
                currSpecial.SpecialLevels.Add(new SpecialLevel());

            if (currSpecial != null)
                DisplayCombo();
        }

        void DisplayCombo()
        {
            GUI.enabled = true;
            GUILayout.BeginVertical("box");
            {
                GUILayout.Label(currSpecial.name + " Special Attack", EditorStyles.boldLabel);

                // Rename Combo File
                GUILayout.BeginHorizontal("label");
                {
                    RenameString = EditorGUILayout.TextField("Special Attack Name", RenameString, GUILayout.ExpandWidth(true));

                    if (GUILayout.Button("Rename"))
                        RenameComboAsset(RenameString);
                }
                GUILayout.EndHorizontal();

                GUILayout.Space(10f);
                GUILayout.Label(currSpecial.SpecialLevels.Count + " Special Levels");
            }
            GUILayout.EndVertical();
            GUILayout.Space(30f);
            GUILayout.Label("Manage Special Levels", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal("label");
            {
                if (GUILayout.Button("Create"))
                    CreateComboStep();

                GUI.enabled = (currSpecial.SpecialLevels.Count > 1);
                if (GUILayout.Button("Delete Last"))
                    DeleteLastComboStep();

                GUILayout.FlexibleSpace();

                GUI.enabled = (currLevelID > 0);
                if (GUILayout.Button("Prev"))
                    currLevelID--;

                GUI.enabled = (currLevelID < currSpecial.SpecialLevels.Count - 1);
                if (GUILayout.Button("Next"))
                    currLevelID++;

                GUI.enabled = true;
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginVertical("box");
            {
                GUILayout.Label("Level " + (currLevelID + 1), EditorStyles.boldLabel);
                GUILayout.Space(10f);

                currSpecial.SpecialLevels[currLevelID].Display();
            }
            GUILayout.EndVertical();

            EditorUtility.SetDirty(currSpecial);
        }

        void CreateComboStep()
        {
            currSpecial.SpecialLevels.Add(new SpecialLevel());
        }

        void DeleteLastComboStep()
        {
            int size = currSpecial.SpecialLevels.Count;
            if (currLevelID == size - 1)
                currLevelID--;
            currSpecial.SpecialLevels.RemoveAt(size - 1);
        }

        void RenameComboAsset(string newName)
        {
            string assetPath = AssetDatabase.GetAssetPath(currSpecial.GetInstanceID());
            AssetDatabase.RenameAsset(assetPath, newName);
            AssetDatabase.SaveAssets();
        }
    }
}
