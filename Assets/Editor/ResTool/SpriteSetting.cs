using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using SFB;

namespace ResTool
{
    public class SpriteSetting : EditorWindow
    {
        private string m_spritePath;
        private string m_outputPath;
        private bool m_isRename;
        private const string m_prefix = "Res_Sprite_";
        private string m_cutStr;

        [MenuItem("Tool/图片设置")]
        private static void ShowWindow()
        {
            EditorWindow.GetWindow<SpriteSetting>().Show();
        }


        private void OnEnable()
        {
            m_spritePath = XMLUtility.Load<string>(m_prefix + "m_spritePath");
            m_outputPath = XMLUtility.Load<string>(m_prefix + "m_outputPath");
            //m_isRename = XMLUtility.Load<bool>(m_prefix + "m_isRename");
            m_cutStr = XMLUtility.Load<string>(m_prefix + "m_cutStr");
        }

        private void OnDisable()
        {
            XMLUtility.Save(m_prefix + "m_spritePath", m_spritePath);
            XMLUtility.Save(m_prefix + "m_outputPath", m_outputPath);
            //XMLUtility.Save(m_prefix + "m_isRename", m_isRename);
            XMLUtility.Save(m_prefix + "m_cutStr", m_cutStr);
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            m_spritePath = EditorGUILayout.TextField("图片地址:", m_spritePath);
            if (GUILayout.Button("浏览", GUILayout.Width(60)))
            {
                var paths = StandaloneFileBrowser.OpenFolderPanel("导出地址", "", true);
                if (paths.Length > 0)
                {
                    m_spritePath = paths[0];

                }
            }
            EditorGUILayout.EndHorizontal();

            // EditorGUILayout.BeginHorizontal();
            // m_outputPath = EditorGUILayout.TextField("导出:", m_outputPath);
            // if (GUILayout.Button("浏览", GUILayout.Width(60)))
            // {
            //     var paths = StandaloneFileBrowser.OpenFolderPanel("导出地址", "", true);
            //     if (paths.Length > 0)
            //     {
            //         m_outputPath = paths[0];

            //     }
            // }
            // EditorGUILayout.EndHorizontal();

            // EditorGUILayout.BeginHorizontal();
            // m_isRename = GUILayout.Toggle(m_isRename, "是否重命名");
            // EditorGUILayout.EndHorizontal();
            // EditorGUILayout.BeginHorizontal();
            // m_cutStr = EditorGUILayout.TextField("分割字符(有多种用;分割):", m_cutStr, GUILayout.Height(30));
            // EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            if (GUILayout.Button("图片转换", GUILayout.Width(100), GUILayout.Height(40)))
            {
                SpriteSwitch();
            }
            EditorGUILayout.Space();
             if (GUILayout.Button("图片重命名", GUILayout.Width(100), GUILayout.Height(40)))
            {
                SpriteRename();
            }
            EditorGUILayout.Space();
            if (GUILayout.Button("设置图片格式", GUILayout.Width(100), GUILayout.Height(40)))
            {
                ResetSprite();
            }
            EditorGUILayout.Space();
            EditorGUILayout.EndHorizontal();

        }

        private void SpriteRename()
        {
            var fileList = GetSpriteList();
            fileList.ForEach(x =>
           {
               var cutstr = "1x1-";
               if (x.FullName.Contains(cutstr))
               {
                   var start_id = x.FullName.IndexOf(x.Name);
                   var cut_id_1 = x.FullName.IndexOf(cutstr) + cutstr.Length;
                   var end_str = x.FullName.Substring(cut_id_1);
                   var cut_id_2 = end_str.IndexOf('-') + 1;
                   var nf = x.FullName.Substring(0, start_id) + end_str.Substring(cut_id_2).Replace('-', '_');
                   //nf = nf.Replace('-', '_');
                   File.Move(x.FullName, nf);
               }
           });
            AssetDatabase.Refresh();
        }

        private void SpriteSwitch()
        {

            var fileList = GetSpriteList();
            fileList.ForEach(x =>
            {
                SetImprt(x.FullName);
                var path = FullPathToAssetPath(x.FullName);
                var tex = AssetDatabase.LoadAssetAtPath(path, typeof(Texture2D)) as Texture2D;
                var colors = tex.GetPixels();
                Texture2D nt = new Texture2D(tex.width, tex.height);

                for (int i = 0; i < colors.Length; i++)
                {
                    var c = colors[i];
                    if (c.GetColorRGB().magnitude < 0.1f)
                    {
                        c.a = 0;
                    }
                    colors[i] = c;
                }
                nt.SetPixels(colors);
                nt.Apply();
                var bytes = nt.EncodeToPNG();
                File.WriteAllBytes(path, bytes);
            });

            AssetDatabase.Refresh();
        }

        private void ResetSprite()
        {
            var fileList = GetSpriteList();
            fileList.ForEach(x =>
            {
                SetImprt(x.FullName);
            });
        }

        private List<FileInfo> GetSpriteList()
        {
            Stack<DirectoryInfo> s = new Stack<DirectoryInfo>();
            s.Push(new DirectoryInfo(m_spritePath));
            List<FileInfo> fileList = new List<FileInfo>();
            while (s.Count > 0)
            {
                var dir = s.Pop();
                var ds = dir.GetDirectories();
                ds.Foreach(x => s.Push(x));
                var fs = dir.GetFiles();
                fs.Foreach(x =>
                {
                    if (x.FullName.EndsWith("png") |
                       x.FullName.EndsWith("jpg"))
                    {
                        fileList.Add(x);
                    }
                });
            }
            return fileList;
        }

        private void SetImprt(string nf)
        {
            var path = FullPathToAssetPath(nf);
            var importer = TextureImporter.GetAtPath(path) as TextureImporter;
            // if (importer == null)
            // {
            //     Debug.Log(path);
            //     return;
            // }
            importer.maxTextureSize = 128;
            importer.spritePixelsPerUnit = 128;
            importer.textureType = TextureImporterType.Sprite;
            importer.mipmapEnabled = false;
            importer.isReadable = true;
            importer.alphaIsTransparency = true;
            AssetDatabase.ImportAsset(path);
        }

        private string FullPathToAssetPath(string nf)
        {
            var index = nf.IndexOf("Assets");
            var path = nf.Substring(index);
            return path;
        }



    }
}