using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BTA
{
    [CustomEditor(typeof(LevelData))]
    public class LevelDataEditor : Editor
    {
        LevelData m_currentLevelData;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            m_currentLevelData = target as LevelData;
            EditorUtility.SetDirty(m_currentLevelData);
        }
    }

}