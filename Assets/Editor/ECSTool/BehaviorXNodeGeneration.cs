﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using SFB;
using UnityEditor;
using UnityEngine;
using System.IO;

namespace ECSTool
{
    public class BehaviorXNodeGeneration : EditorWindow
    {
        private string m_outputPath;
        private const string m_savePrefix = "ECS_XNode_";
         
        [MenuItem("Tool/生成节点")]
        private static void ShowWindow()
        {
            EditorWindow.GetWindow<BehaviorXNodeGeneration>().Show();
        }

        public void OnEnable() 
        {
            LoadValue();
        }

        private void LoadValue()
        {
             m_outputPath = XMLUtility.Load<String>(m_savePrefix + "m_outputPath");
        }


        private void OnGUI() 
        {
            EditorGUILayout.BeginHorizontal();
            m_outputPath = EditorGUILayout.TextField("导出地址:", m_outputPath);
            if(GUILayout.Button("浏览", GUILayout.Width(60)))
            {
                OnOutputPathBorwseButtonDown();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            if(GUILayout.Button("生成节点", GUILayout.Height(40), GUILayout.Width(100)))
            {
                OnNodeGenerationButtonDown();
            }
            EditorGUILayout.Space();
            EditorGUILayout.EndHorizontal();
        }

        private void OnOutputPathBorwseButtonDown()
        {
             var paths = StandaloneFileBrowser.OpenFolderPanel("导出地址", "", true);
            if(paths.Length > 0)
            {
                m_outputPath = paths[0];
                XMLUtility.Save(m_savePrefix + "m_outputPath", m_outputPath);
            }
        }

        // private void Clear()
        // {
        //     var fs = Directory.GetFiles(m_outputPath, "*.cs", SearchOption.AllDirectories);
        //     for (int i = 0; i < fs.Length; i++)
        //     {
        //         Debug.Log(fs[i]);
        //         Directory.Delete(fs[i]);
        //     }
            
        // }

        private void OnNodeGenerationButtonDown()
        {
            if(string.IsNullOrEmpty(m_outputPath))
            {
                return;
            }
            var types = Assembly.Load("Assembly-CSharp").GetTypes();
            for (int i = 0; i < types.Length; i++)
            {
                var t = types[i];
                var att = t.GetCustomAttribute(typeof(BehaviorNodeAttribute), false) as BehaviorNodeAttribute;
                if(att != null)
                {
                    string contents = @"using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class X{name_type} : {base_type}
    {
{vars}
        
        public override BehaviorNode GetBehaviorNode()
        {
            return new {name_type}({args}GetChilds());
        }
    }
}";
                    
                    var res = t.ToString().Split('.');
                    string name_type = res[res.Length - 1];
                    string vars = "";
                    string args = "";
                    string base_type = att.IsHasNext ? typeof(BT.XBehaviorLinkNode).ToString() : typeof(BT.XBehaviorNode).ToString();
                    string path = $"{m_outputPath}/X{name_type}.cs";
                    var fs = t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                    for (int n = 0; n < fs.Length; n++)
                    {
                        var f = fs[n];
                        var nt = f.GetCustomAttribute(typeof(SerializableNodeFieldAttribute), false);
                        if(nt == null)
                        {
                            continue;
                        }
                        var fname = f.Name.Replace("m_", "");
                        fname = fname.Substring(0,1).ToUpper() + fname.Substring(1); 
                        vars += $"\t\tpublic {f.FieldType} {fname};\n";
                        args += $"{fname}, ";
                    }
                    contents = contents.Replace("{name_type}", name_type).Replace("{args}", args).Replace("{vars}", vars)
                        .Replace("{base_type}", base_type);
                    if(!att.IsHasNext)
                    {
                        var str = args == ""? "GetChilds()" : ", GetChilds()";
                        contents = contents.Replace(str, "");
                    }
                    File.WriteAllText(path, contents);
                }
                if(t.GetInterface("IBehaviorData") != null)
                {
                    string contents = @"using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class X{name_part}ActionNode : XActionNode<{name_type}>
    {

    }
}";             
                    var res = t.ToString().Split('.');
                    string name_type = res[res.Length - 1];
                    string name_part = name_type.Replace("Data", "");
                    string path = $"{m_outputPath}/X{name_part}ActionNode.cs";
                    contents = contents.Replace("{name_type}", name_type).Replace("{name_part}", name_part);
                    File.WriteAllText(path, contents);
                }
                AssetDatabase.Refresh();

            }
            
        }
    }

}
