#if UNITY_EDITOR

using System;
using UnityEditor;
using UnityEngine;

namespace GitToTitle
{
    [ExecuteInEditMode]
    public static class TitleChangeEditor
    {
        const int Interval = 1;
        static long lastUpdateTime;

        [InitializeOnLoadMethod]
        private static void AttachTitleChangeLoop ()
        {
            EditorApplication.update += TitleChangeLoop;
        }

        private static void TitleChangeLoop ()
        {
            // DateTime.Ticksの単位は100ナノ秒
            var second = DateTime.Now.Ticks / (10 * 1000 * 1000);
            if (second < (lastUpdateTime + Interval))
                return;

            lastUpdateTime = second;
            TitleChanger.TitleChange ();
        }
    }
}

#endif