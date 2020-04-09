using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BTA
{
    [CustomEditor(typeof(FlaskData))]
    public class FlaskEditor : Editor
    {
        FlaskData m_currentFlask;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            m_currentFlask = target as FlaskData;
            EditorUtility.SetDirty(m_currentFlask);
        }
    }

}