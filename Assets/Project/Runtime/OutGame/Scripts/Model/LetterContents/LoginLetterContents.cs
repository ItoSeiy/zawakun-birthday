using System;
using UnityEngine;

namespace Project.Runtime.OutGame.Model
{
    [Serializable]
    public class LoginLetterContents
    {

        [Serializable]
        public class Contents
        {
            [SerializeField]
            private ContentsType _contentsType;
            
            [SerializeField]
            [TextArea]
            private string _text;

            public ContentsType ContentsType => _contentsType;

            public string Text => _text;
        }
        
        public enum ContentsType
        {
            
        }
    }
}