using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GitToTitle
{
    public static class TitleChanger
    {
#if UNITY_STANDALONE_WIN
        [DllImport ("user32.dll", EntryPoint = "SetWindowText")]
        public static extern bool SetWindowText (System.IntPtr hwnd, System.String lpString);
        [DllImport ("user32.dll")]
        private static extern System.IntPtr GetActiveWindow ();

        public static void TitleChange (string text)
        {
            IntPtr windowPtr = GetActiveWindow ();
            SetWindowText (windowPtr, text);
        }

#if UNITY_EDITOR
        public static void TitleChange ()
        {
            TitleChange (TitleText ());
        }
        public static string TitleText ()
        {
            var scene = SceneManager.GetActiveScene ().name;
            var subInfo = Application.dataPath;
            return TitleText (scene, subInfo);
        }
        public static string TitleText (string mainInfo, string subInfo)
        {
            var product = Application.productName;
            var unityVersion = $"[Unity{Application.unityVersion}]";

            var gitInfo = GitHelper.Info ();
            if (gitInfo == null)
                return $"{product} - {mainInfo} - {subInfo} - {unityVersion}";

            var git = $"{gitInfo.branch}<{gitInfo.hash}>";
            return $"{product} - {mainInfo} - {git} - {subInfo} - {unityVersion}";
        }
#endif
#else
    public static void TitleChange (string text) { }
    public static void TitleChange () { }
    public static void TitleText () { return "not supported"; }
    public static void TitleText (string scene) { return "not supported"; }
#endif
    }
}