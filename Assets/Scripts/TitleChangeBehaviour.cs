using UnityEngine;

namespace GitToTitle
{
    public class TitleChangeBehaviour : MonoBehaviour
    {
        public const string textFileName = "titleBarText";

        // ビルド前に設定され、ビルド後に初期化される
        [HideInInspector]
        public static string titleText = "";

        // タイトルバーに埋め込む各シーン名的な物
        [SerializeField]
        string _buildName = "";
        public string buildName => _buildName;


        void Start()
        {
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
            // テキストアセットをロードしてコンテンツを表示
            TextAsset textAsset = Resources.Load<TextAsset>(textFileName);
            Debug.Log(textAsset.text);
            TitleChanger.TitleChange (textAsset.text);

//            TitleChanger.TitleChange (titleText);
            Debug.Log($"textAsset:{textAsset.text}");
            Debug.Log($"titleText:{titleText}");
#endif
        }

        public void Set (string text)
        {
            Debug.Log($"text={text}");
            titleText = text;
        }

        public void Clear ()
        {
            titleText = "";
        }
    }
}