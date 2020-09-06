using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrameLearn
{
    public class EditorPlatform : EditorWindow
    {
        private ModuleContainer moduleContainer;


        [MenuItem("FrameLearn/EditorModulePlatform")]
        public static void OpenWindow()
        {
            var window = GetWindow<EditorPlatform>();

            window.position = new Rect()
            {
                position = new Vector2(Screen.width * 0.5f, Screen.height * 0.6f),
                size = new Vector2(600, 500),
            };

            var moduleType = typeof(IEditorPlatformModule);

            window.moduleContainer = new ModuleContainer(new DefaultModuleCache(),
                new AssmeblyModuleFactory(moduleType.Assembly, moduleType));


            window.Show();
        }


        private void OnGUI()
        {
            var modules = moduleContainer.GetAllModules<IEditorPlatformModule>();

            foreach (var module in modules)
            {
                module.OnGUIDraw();
            }
        }
    }
}