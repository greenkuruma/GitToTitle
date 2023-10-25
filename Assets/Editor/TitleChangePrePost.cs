using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.WSA;

namespace GitToTitle
{
    public class TitleChangePrePost : IPreprocessBuildWithReport, IPostprocessBuildWithReport
    {
        public int callbackOrder { get { return 0; } }

        private string _resourcesDirectoryPath = "Assets/GitToTitleTemp/Resources/";
        private string _resourcesDirectoryStartPath = "Assets/GitToTitleTemp";

        // ファイルの保存先パス
        // テキストアセットとして保存するため、拡張子を".txt"に
        string filePath => _resourcesDirectoryPath + TitleChangeBehaviour.textFileName + ".txt";

        public void OnPreprocessBuild (BuildReport report)
        {
            // @todo アクティブなシーン名いれたい…けどまあいいや
            Debug.Log("OnPreprocessBuild");

            var titleChangeBehaviour = GameObject.FindObjectOfType<TitleChangeBehaviour> ();
            string buildName = titleChangeBehaviour != null
                ? titleChangeBehaviour.buildName
                : "";
            var titleText = TitleChanger.TitleText(buildName, $"build:{DateTime.Now.ToString()}");
            CreateAndWriteTextFile(titleText);
        }
        public void CreateAndWriteTextFile(string titleText)
        {
            // フォルダがなければ作成
            if (! Directory.Exists(_resourcesDirectoryPath))
                Directory.CreateDirectory(_resourcesDirectoryPath);

            // テキストファイルを作成し、内容を書き込む
            File.WriteAllText(filePath, titleText);

            Debug.Log($"OnPreprocessBuild:titleText={titleText}");
        }

        public void OnPostprocessBuild (BuildReport report)
        {
            Debug.Log("OnPostprocessBuild");

            Directory.Delete(_resourcesDirectoryStartPath, true);
            File.Delete(_resourcesDirectoryStartPath + ".meta");
        }
    }
}