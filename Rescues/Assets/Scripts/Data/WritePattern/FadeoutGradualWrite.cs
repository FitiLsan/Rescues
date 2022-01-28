using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Rescues
{
    [CreateAssetMenu(fileName = "FadeoutGradualWrite", menuName = "Data/WritePatterns/FadeoutGradualWrite")]
    public sealed class FadeoutGradualWrite : WritePattern
    {
        #region Fields

        public const int WRITE_SPEED_MAX_RANGE = 10;
        public const int WRITE_STEP_MAX_RANGE = 10;

        [Header("Gradual settings")]
        [Range(1, WRITE_STEP_MAX_RANGE)]
        [SerializeField] private int _writeStep;
        [Range(1, WRITE_SPEED_MAX_RANGE)]
        [SerializeField] private int _writeSpeed;
        [Header("Fade settings")]
        [SerializeField] private float _timeToFadeout;
        [SerializeField] private float _timeBeforeFadeoutStarts;
        [Header("Optional settings")]
        [Range(0, 1)]
        [SerializeField] private float _maxAlpha = 1;
        [Range(0, 1)]
        [SerializeField] private float _minAlpha = 0;

        private readonly List<ITimeRemaining> _sequence = new List<ITimeRemaining>();

        #endregion


        #region Methods

        public override void DrawText(string inputText, TextMeshProUGUI outputTextContainer)
        {
            ClearText(outputTextContainer);
            RestoreAlpha(outputTextContainer);

            _sequence.Clear();
            float _timeForWriteChar = Time.deltaTime * WRITE_SPEED_MAX_RANGE / _writeSpeed;
            int start = 0;
            int tempStep = _writeStep;
            while (start < inputText.Length)
            {
                if ((start + tempStep) >= inputText.Length)
                {
                    tempStep = inputText.Length - start;
                }

                string tempSubstring = inputText.Substring(start, tempStep);
                _sequence.Add(new TimeRemaining(() =>
                {
                    outputTextContainer.text += tempSubstring;
                },
                _timeForWriteChar));

                start += tempStep;
            }

            if (_sequence.Count > 0)
            {
                Fade(outputTextContainer);
                _sequence.AddSequentialTimeRemaining();
            }
        }

        public override void ClearText(TextMeshProUGUI textContainer)
        {
            _sequence.RemoveSequentialTimeRemaining();            
            textContainer.text = string.Empty;
        }

        private void Fade(TextMeshProUGUI outputTextContainer)
        {
            _sequence.Add(new TimeRemaining(() =>
            {
                outputTextContainer.DOFade(_minAlpha, _timeToFadeout).
                OnComplete(() =>
                {
                    outputTextContainer.text = string.Empty;
                    RestoreAlpha(outputTextContainer);
                });
            },
            _timeBeforeFadeoutStarts));
        }

        private void RestoreAlpha(TextMeshProUGUI textContainer)
        {
            DOTween.Kill(textContainer);
            textContainer.DOFade(_maxAlpha, 0);
        }

        #endregion
    }
}