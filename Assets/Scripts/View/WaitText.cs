using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace View
{
    [RequireComponent(typeof(TMP_Text))]
    public class WaitText : MonoBehaviour
    {
        public string original = "Wait Request Create Account";
        public string suffixes = ".";
        public int suffixesAmount = 3;
        public float interval = 0.25f;

        private readonly List<string> _textFrames = new();
        private TMP_Text _text;


        private void Awake()
        {
            _text = GetComponent<TMP_Text>();

            for (var i = 0; i <= suffixesAmount; i++)
            {
                var iFarm = $"{original}";
                for (var s = 0; s < i; s++) iFarm += suffixes;
                _textFrames.Add(iFarm);
            }
        }

        private void OnEnable()
        {
            StartCoroutine(Present());
        }


        private int _currentFrame = 0;

        private IEnumerator Present()
        {
            while (gameObject.activeInHierarchy)
            {
                _text.SetText(_textFrames[_currentFrame]);
                yield return new WaitForSeconds(interval);
                _currentFrame = (int)Mathf.Repeat(++_currentFrame, _textFrames.Count);
            }
        }
    }
}