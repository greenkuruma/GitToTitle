using System;
using System.IO;
using System.Security.Policy;
using UnityEngine;

namespace GitToTitle
{
    public static class GitHelper
    {
        public class GitInfo
        {
            public string branch;
            public string hash;
        }

        public static GitInfo Info ()
        {
            var gitPath = $"{Application.dataPath}/../.git";
            var headFilePath = $"{gitPath}/HEAD";

            // そもそも狙っているファイルが無ければnull
            if (! File.Exists (headFilePath))
                return null;

            var head = Text.Read (headFilePath, true);
            GitInfo info = null;

            // branchを参照している
            if (head.Contains("ref:"))
            {
                // "ref: refs/heads/hogeBranch"みたいな感じなので、Pathだけにする
                head = head.Replace ("ref: ", "").Replace ("\r", "").Replace ("\n", "");

                var branchPath = $"{gitPath}/{head}";
                var hash = Text.Read (branchPath, true);

                info = new GitInfo () { branch = head, hash = hash };
            }
            // branchを参照していない
            else
            {
                // headにhashが入ってる
                info = new GitInfo () { branch = "no referencing branch", hash = head };
            }
            // 改行が入ってることがあるので消す
            info.hash = info.hash.Replace("\r", "").Replace("\n", "");

            return info;
        }
    }
    public static class Text
    {
        public static string Read (string path, bool editorOnly = false)
        {
    #if !UNITY_EDITOR
                if (editorOnly)
                    return null;
    #endif // #if !UNITY_EDITOR
            if (string.IsNullOrEmpty (path))
            {
                Debug.LogWarning ("path null or empty");
                return null;
            }

            // 冗長ではあるが、StreamWriterを開く際のExceptionも考慮したtry節
            string text = null;
            try
            {
                using (StreamReader reader = new StreamReader (path, false))
                {
                    try
                    {
                        text = reader.ReadToEnd ();
                    }
                    catch (Exception e)
                    {
                        Debug.LogWarning (e);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning (e);
            }

            return text;
        }
    }
}