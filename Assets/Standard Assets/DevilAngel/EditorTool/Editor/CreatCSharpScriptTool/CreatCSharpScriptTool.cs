using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEditor.ProjectWindowCallback;
using System.Text;
using System.Text.RegularExpressions;

namespace DevileAngel.EditorTool
{
    internal class CreatCSharpScript
    {
        private const string DEFINE_CLASS_NAME = "NewClass";
        private const string DEFINE_SCRIPT = "using UnityEngine;" +
                    "\n" +
                    "\nnamespace NullNamespace" +
                    "\n{" +
                    "\n\tpublic class #ScriptName#" +
                    "\n\t{" +
                    "\n\t\t" +
                    "\n\t}" +
                    "\n}";

        [MenuItem("Assets/Create/C# Script Self", false, 80)]
        private static void OpenCreatCSharpScript()
        {
            string pathName = Utilits.GetSelectionPath()[0] + "/" + DEFINE_CLASS_NAME;

            Texture2D icon = EditorGUIUtility.IconContent("cs Script Icon").image as Texture2D;

            var scriptAction = Utilits.GetUnityObjectIndexOf<CreatCSarpScriptAction>();
            if (scriptAction == null) scriptAction = ScriptableObject.CreateInstance<CreatCSarpScriptAction>();
           
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, scriptAction, pathName, icon, string.Empty);
        }


        private class CreatCSarpScriptAction : EndNameEditAction
        {
            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                var scriptName = Path.GetFileName(pathName);
                if (scriptName.Equals(DEFINE_CLASS_NAME)) return;

                pathName = pathName + ".cs";

                var encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: true);
                string script = GetScript(scriptName);

                script = IsClassOrInterface(script, scriptName);

                SetLineEndings(script, LineEndingsMode.Windows);

                File.WriteAllText(pathName, script, encoding);

                AssetDatabase.ImportAsset(pathName);

                var obj = AssetDatabase.LoadAssetAtPath(pathName, typeof(UnityEngine.Object));

                ProjectWindowUtil.ShowCreatedAsset(obj);
            }

            private string GetScript(string scriptName)
            {
                return DEFINE_SCRIPT.Replace("#ScriptName#", scriptName);
            }

            private string IsClassOrInterface(string script, string scriptName)
            {

                if (scriptName.StartsWith("I"))
                {
                    if (scriptName.Length > 2)
                    {
                        var name = scriptName[1];
                        if (name > 'A' && name < 'Z')
                        {
                            script = script.Replace("class", "interface");
                        }
                    }
                }
                return script;
            }

            private string SetLineEndings(string content, LineEndingsMode lineEndingsMode)
            {
                string replacement;
                switch (lineEndingsMode)
                {
                    case LineEndingsMode.OSNative:
                        replacement = ((Application.platform != RuntimePlatform.WindowsEditor) ? "\n" : "\r\n");
                        break;
                    case LineEndingsMode.Unix:
                        replacement = "\n";
                        break;
                    case LineEndingsMode.Windows:
                        replacement = "\r\n";
                        break;
                    default:
                        replacement = "\n";
                        break;
                }

                content = Regex.Replace(content, "\\r\\n?|\\n", replacement);
                return content;
            }
        }

    }
}