using System;
using System.Linq;
using NaughtyAttributes;
using Project.Framework.Utils;
using UnityEngine;

namespace Project.Runtime.OutGame.Model
{
    [CreateAssetMenu]
    public class LetterContents : ScriptableObject
    {
        private const string UserName = "{userName}";

        [SerializeField]
        private ContentsParent[] _contentsParentArray;

        public ContentsParent GetContentsParent(ContentsParentType type)
        {
            var parent = _contentsParentArray.FirstOrDefault(x => x.ContentsParentType == type);
            if (parent != null)
            {
                return parent;
            }

            CustomDebug.LogError($"[{nameof(LetterContents)}] 指定されたType:{type}のテキストの親が見つかりませんでした。");
            return null;
        }

        [Serializable]
        public class ContentsParent
        {
            [SerializeField]
            private ContentsParentType _parentType;

            [SerializeField]
            private Contents[] _contentsArray;

            public ContentsParentType ContentsParentType => _parentType;

            public Contents GetContents(ContentsType type)
            {
                var contents = _contentsArray.FirstOrDefault(x => x.ContentsType == type);
                if (contents != null)
                {
                    return contents;
                }

                CustomDebug.LogError($"[{nameof(ContentsParent)}] 指定されたType:{type}のテキストが見つかりませんでした。");
                return null;
            }

            [Serializable]
            public class Contents
            {
                [SerializeField]
                private ContentsSfbType _sfbType;

                [SerializeField]
                private ContentsType _contentsType;

                [ShowIf(nameof(_sfbType), ContentsSfbType.Save)]
                [SerializeField]
                private float _waitForSeconds;

                [ShowIf(nameof(_sfbType), ContentsSfbType.Open)]
                [SerializeField]
                private string _matchPattern;

                [ResizableTextArea]
                [SerializeField]
                private string _text;

                [ShowIf(nameof(_sfbType), ContentsSfbType.Open)]
                [ResizableTextArea]
                [SerializeField]
                private string _fallBackText;

                public ContentsSfbType SfbType => _sfbType;

                public ContentsType ContentsType => _contentsType;

                public float WaitForSeconds => _waitForSeconds;

                public string MatchPattern => _matchPattern;

                public string Text => _text;

                public string FallBackText => _fallBackText;
            }
        }
    }
}