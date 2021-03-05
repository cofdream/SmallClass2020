using UnityEngine;
using UnityEditor;

namespace NullNamespace
{
    public class TestWindow : EditorWindow
    {
        [MenuItem("Test/Open")]
        static void Open()
        {
            GetWindow<TestWindow>().Show();
        }


        private void OnGUI()
        {
            GUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();
                GUILayout.Label("Title");
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndHorizontal();
        }
    }
}