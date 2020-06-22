using UnityEngine;

namespace GitToTitle
{
    public class TitleChangeBehaviour : MonoBehaviour
    {
        // ビルド前に設定され、ビルド後に初期化される
        [HideInInspector]
        public string titleText = "";

        // タイトルバーに埋め込む各シーン名的な物
        [SerializeField]
        string _buildName = "";
        public string buildName => _buildName;

        void Start ()
        {
            TitleChanger.TitleChange (titleText);
        }

        public void Set (string text)
        {
            titleText = text;
        }

        public void Clear ()
        {
            titleText = "";
        }
    }
}