using System.IO;
using System;
using UnityEngine;
using UnityEditor;

namespace DevilAngel.EditorTool
{
    public static  class EditorTools
    {
        #region Export Package

        [MenuItem("Tools/Export Package", true)]
        private static bool IsExportPackag()
        {
            var assetPath = Application.dataPath + "/!_Tools";
            return Directory.Exists(assetPath);
        }

        [MenuItem("Tools/Export Package", false, 1)]
        private static void ExportPackage()
        {
            EditorUtility.DisplayProgressBar("Export Package", "Wait Export!", 0);

            var assetPath = "Assets/!_Tools";
            var fileName = string.Format("Tools_{0}.unitypackage", DateTime.Now.ToString("yyyyMMdd-HH-mm-ss"));

            AssetDatabase.ExportPackage(assetPath, fileName, ExportPackageOptions.Recurse);

            //把导出的包移动到指定路径存放
            var basePath = Directory.GetParent(Application.dataPath).FullName;
            var fileSavePath = basePath + "/Export Packages";
            if (Directory.Exists(fileSavePath) == false)
            {
                Directory.CreateDirectory(fileSavePath);
            }

            var filePath = basePath + @"\" + fileName;

            if (File.Exists(filePath))
            {
                File.Move(filePath, fileSavePath + @"\" + fileName);
            }

            EditorUtility.DisplayProgressBar("Export Package", "Export The End!", 1);
            EditorUtility.ClearProgressBar();

            Application.OpenURL("file://" + fileSavePath);

            //EditorUtility.DisplayDialog("Export Packerage", "导出工具包已完成，路径已复制到粘贴板！", "确定");
            //GUIUtility.systemCopyBuffer = ;
        }
        #endregion
    }
}