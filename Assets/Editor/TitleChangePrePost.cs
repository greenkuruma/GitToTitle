using System;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace GitToTitle
{
    public class TitleChangePrePost : IPreprocessBuildWithReport, IPostprocessBuildWithReport
    {
        public int callbackOrder { get { return 0; } }

        public void OnPreprocessBuild (BuildReport report)
        {
            // @todo アクティブなシーン名いれたい…けどまあいいや

            // title文字列設定
            var titleChangeBehaviour = GameObject.FindObjectOfType<TitleChangeBehaviour> ();
            if (titleChangeBehaviour != null)
                titleChangeBehaviour.Set (
                    TitleChanger.TitleText (titleChangeBehaviour.buildName, $"build:{DateTime.Now.ToString ()}"));
        }

        public void OnPostprocessBuild (BuildReport report)
        {
            var titleChangeBehaviour = GameObject.FindObjectOfType<TitleChangeBehaviour> ();
            if (titleChangeBehaviour != null)
                titleChangeBehaviour.Clear ();
        }
    }
}