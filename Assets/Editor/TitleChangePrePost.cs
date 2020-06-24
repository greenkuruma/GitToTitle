using System;
using UnityEditor.Build;
using UnityEngine;

namespace GitToTitle
{
#if UNITY_2018_1_OR_NEWER
    using UnityEditor.Build.Reporting;

    public class TitleChangePrePost : IPreprocessBuildWithReport, IPostprocessBuildWithReport
    {
        public int callbackOrder => 0;

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
#else
    using UnityEditor;

    public class TitleChangePrePost : IPreprocessBuild, IPostprocessBuild
    {
        public int callbackOrder => 0;

        public void OnPreprocessBuild (BuildTarget target, string path)
        {
            // title文字列設定
            var titleChangeBehaviour = GameObject.FindObjectOfType<TitleChangeBehaviour> ();
            if (titleChangeBehaviour != null)
                titleChangeBehaviour.Set (
                    TitleChanger.TitleText (titleChangeBehaviour.buildName, $"build:{DateTime.Now.ToString ()}"));
        }

        public void OnPostprocessBuild (BuildTarget target, string path)
        {
            var titleChangeBehaviour = GameObject.FindObjectOfType<TitleChangeBehaviour> ();
            if (titleChangeBehaviour != null)
                titleChangeBehaviour.Clear ();
        }

    }
#endif
}