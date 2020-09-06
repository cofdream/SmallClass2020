using UnityEditor;
using UnityEngine;

namespace FrameLearn
{
    public class ModuleContainerModule : IEditorPlatformModule
    {
        public void OnGUIDraw()
        {
            GUILayout.Label("ModuleContainerModule",EditorStyles.boldLabel);
        }
    }
}