using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace DevilAngel.EditorTool
{
    [CustomEditor(typeof(RectTransform), true), CanEditMultipleObjects]
    public class RectTransformInspector : Editor
    {
        //private readonly GUIContent contentPosition = new GUIContent(" P ", (Texture)null, "当前物体的本地坐标归0");
        private readonly GUIContent contentRotation = new GUIContent(" R ", (Texture)null, "当前物体的本地旋转归0");
        private readonly GUIContent contentScale = new GUIContent(" S ", (Texture)null, "当前物体的本地缩放归1");
        private readonly GUIContent contentAddWindow = new GUIContent("增量修改");

        //private bool isResetPosition;
        private bool isResetRotation;
        private bool isResetScale;
        private bool isOpenAddWindow;

        #region 增量面板
        private static RectTransformExpand rectTranExpand;//自定义数据类，不使用基类的SerializedObject。

        private SerializedObject m_serializedObject;

        private SerializedProperty serializedPropertyPosition;
        //private SerializedProperty serializedPropertyRotation;
        //private SerializedProperty serializedPropertyScale;

        private bool isStartAdd;
        #endregion

        private Editor defaultEditor;

        private void Awake()
        {
            var objs = Resources.FindObjectsOfTypeAll<RectTransformExpand>();
            if (objs.Length != 0)
            {
                rectTranExpand = objs[0];
            }
            else
            {
                rectTranExpand = ScriptableObject.CreateInstance<RectTransformExpand>();
            }

            m_serializedObject = new SerializedObject(rectTranExpand);
        }

        public void OnEnable()
        {
            defaultEditor = Editor.CreateEditor(targets, Type.GetType("UnityEditor.RectTransformEditor, UnityEditor"));

            serializedPropertyPosition = m_serializedObject.FindProperty("addPosition");

            // serializedPropertyRotation = m_serializedObject.FindProperty("addRotation");
            //
            // serializedPropertyScale = m_serializedObject.FindProperty("addScale");
        }
        private void OnDisable()
        {
            MethodInfo disableMethod = defaultEditor.GetType().GetMethod("OnDisable", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            if (disableMethod != null)
                disableMethod.Invoke(defaultEditor, null);

            DestroyImmediate(defaultEditor);
        }
        public override void OnInspectorGUI()
        {
            defaultEditor.OnInspectorGUI();

            DrawOnInspectorGUI();
        }

        private void DrawOnInspectorGUI()
        {
            GUILayout.BeginHorizontal();
            {
                isOpenAddWindow = GUILayout.Toggle(isOpenAddWindow, contentAddWindow, EditorStyles.foldoutHeader);

                //isResetPosition = GUILayout.Button(contentPosition);
                isResetRotation = GUILayout.Button(contentRotation);
                isResetScale = GUILayout.Button(contentScale);
                GUILayout.FlexibleSpace();

                isStartAdd = GUILayout.Button("开始增量");
            }
            GUILayout.EndHorizontal();


            if (isOpenAddWindow)
            {
                m_serializedObject.Update();

                GUILayout.BeginHorizontal();

                GUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(serializedPropertyPosition, new GUIContent("LocalPosition"));

                GUILayout.EndHorizontal();

                if (GUILayout.Button("重置"))
                {
                    rectTranExpand.addPosition = Vector3.zero;
                }

                GUILayout.EndHorizontal();


                // EditorGUI.BeginChangeCheck();
                //
                // GUILayout.BeginHorizontal();
                //
                // GUILayout.BeginHorizontal();
                // EditorGUILayout.PropertyField(serializedPropertyRotation, new GUIContent("Rotation"));
                // GUILayout.EndHorizontal();
                //
                // if (GUILayout.Button("重置"))
                // {
                //     addTransform.addRotation = Vector3.zero;
                // }
                //
                // GUILayout.EndHorizontal();
                //
                //
                // EditorGUI.BeginChangeCheck();
                //
                // GUILayout.BeginHorizontal();
                //
                // GUILayout.BeginHorizontal();
                // EditorGUILayout.PropertyField(serializedPropertyScale, new GUIContent("Scale"));
                // GUILayout.EndHorizontal();
                //
                // if (GUILayout.Button("重置"))
                // {
                //     addTransform.addScale = Vector3.zero;
                // }
                //
                // GUILayout.EndHorizontal();


                m_serializedObject.ApplyModifiedProperties();


            }
            foreach (var temp in targets)
            {
                var rectTransform = temp as RectTransform;
                //
                // if (isResetPosition)
                // {
                //     Undo.RecordObject(rectTransform, "Reset localPosition");
                //     rectTransform.localPosition = Vector3.zero;
                // }

                if (isResetRotation)
                {

                    Undo.RecordObject(rectTransform, "Reset localRotation");
                    rectTransform.localRotation = Quaternion.identity;

                    SerializedProperty m_LocalEulerAnglesHint = this.serializedObject.FindProperty("m_LocalEulerAnglesHint");
                    this.serializedObject.Update();
                    m_LocalEulerAnglesHint.vector3Value = Vector3.zero;
                    this.serializedObject.ApplyModifiedProperties();
                }

                if (isResetScale)
                {
                    Undo.RecordObject(rectTransform, "Reset localScale");
                    rectTransform.localScale = Vector3.one;
                }

                if (isStartAdd)
                {
                    Undo.RecordObject(rectTransform, "Add Change Transform Values");
                    rectTransform.localPosition += rectTranExpand.addPosition;
                    // rectTransform.localEulerAngles += addTransform.addRotation;
                    // rectTransform.localScale += addTransform.addScale;
                }
            }

        }


        private class RectTransformExpand : ScriptableObject
        {
            public Vector3 addPosition;
            // public Vector3 addRotation;
            // public Vector3 addScale;
        }
    }
}