using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SFB;
using System.IO;
using OfficeOpenXml;

namespace ECSTool
{
    public class ComponentGeneration : EditorWindow
    {
        private string m_excelPath;
        private string m_outputPath;
        private const string m_savePrefix = "ECS_CG_";

        [MenuItem("Tool/ECS/ComponentGeneration")]
        private static void ShowWindow()
        {
            EditorWindow.GetWindow<ComponentGeneration>().Show();
            
        }

        public void OnEnable() 
        {
            LoadValue();
        }

        private void LoadValue()
        {
            m_excelPath = PlayerPrefs.GetString(m_savePrefix + "m_excelPath", m_excelPath);
            m_outputPath = PlayerPrefs.GetString(m_savePrefix + "m_outPutPath", m_outputPath);
        }
        
        private void OnGUI() 
        {
            EditorGUILayout.BeginHorizontal();
            m_excelPath = EditorGUILayout.TextField("配置地址:",m_excelPath);
            if(GUILayout.Button("浏览", GUILayout.Width(60)))
            {
                OnExcelPathBorwseButtonDown();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            m_outputPath = EditorGUILayout.TextField("导出地址:", m_outputPath);
            if(GUILayout.Button("浏览", GUILayout.Width(60)))
            {
                OnOutPutlPathBorwseButtonDown();
            }
            EditorGUILayout.EndHorizontal();
            //EditorGUILayout.Space(40);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            if(GUILayout.Button("生成", GUILayout.Width(100)))
            {
                OnGenerationButtonDown();
            }
            EditorGUILayout.Space();
            EditorGUILayout.EndHorizontal();
        }

        private void OnExcelPathBorwseButtonDown()
        {
            var paths = StandaloneFileBrowser.OpenFilePanel("配置地址","", "xlsx", true);
            if(paths.Length > 0)
            {
                m_excelPath = paths[0];
                PlayerPrefs.SetString(m_savePrefix + "m_excelPath", m_excelPath);
            }
        }

         private void OnOutPutlPathBorwseButtonDown()
        {
            var paths = StandaloneFileBrowser.OpenFolderPanel("导出地址", "", true);
            if(paths.Length > 0)
            {
                m_outputPath = paths[0];
                PlayerPrefs.SetString(m_savePrefix + "m_outPutPath", m_outputPath);
            }
        }

        private void OnGenerationButtonDown()
        {
            ExcelPackage package = new ExcelPackage(new FileStream(m_excelPath.Replace('/','\\'), FileMode.Open));            
            ExcelWorksheet sheet = package.Workbook.Worksheets[1];
            int startRow = sheet.Dimension.Start.Row;
            int endRow = sheet.Dimension.End.Row + 1;
            int stratColumn = sheet.Dimension.Start.Column;
            int endColumn = sheet.Dimension.End.Column + 1;
            for (int i = startRow; i < endRow; i++)
            {
                string contents = @"using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct {name_data} : IComponentData
{
{var_dec}
}
";              
                string name_data = "";
                string var_dec = ""; 
                for (int j = stratColumn; j < endColumn; j++)
                {
                    var value = sheet.GetValue(i, j)?.ToString();
                    
                    if(string.IsNullOrEmpty(value))
                    {
                        continue;
                    }
                    if(j == stratColumn)
                    {
                        if(value.StartsWith("//"))
                        {
                            continue;
                        }
                        name_data = value;
                    }
                    else
                    {
                        var res = value.Split(' ');
                        if(res.Length != 2)
                        {
                            continue;
                        }
                        var_dec += $"\tpublic {res[0]} {res[1]};\n";
                    }
                }
                if(string.IsNullOrEmpty(var_dec))
                {
                    continue;
                }
                contents = contents.Replace("{name_data}", name_data).Replace("{var_dec}", var_dec);
                string path = $"{m_outputPath}\\{name_data}.cs";
                File.WriteAllText(path, contents);
            }
            sheet.Dispose();
            package.Dispose();
            AssetDatabase.Refresh();
            Debug.Log("完成");
            Close();
        }


       
    }
}
