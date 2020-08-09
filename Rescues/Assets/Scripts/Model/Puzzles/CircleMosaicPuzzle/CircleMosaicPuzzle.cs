using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rescues
{
    public class CircleMosaicPuzzle : Puzzle
    {
        #region Fileds

        private const float BUTTON_OFFSET = 30;
        private const string LEFT_IN_NAME = "Left";
        private List<RotatingCircle> _circles = new List<RotatingCircle>();
        private RotatingCircle _selectedCircle;
        private Button[] _buttons;
        private Dictionary<RotatingCircle, Rules[]> _rules = new Dictionary<RotatingCircle, Rules[]>();

        #endregion


        #region  Propeties

        public List<RotatingCircle> Circles
        {
            get => _circles;
        }

        #endregion

        #region Methods

        public void Initialize(CircleMosaicData data)
        {
            var circlesData = data.Circles;

            CreateRotatingCircles(circlesData.Length);

            for (int i = 0; i < circlesData.Length; i++)
            {
                InitializeCircle(_circles[i], data.Angle, circlesData[i]);
            }
        }

        private void InitializeCircle(RotatingCircle circle, int rotationAngle, CircleMoveScheme scheme)
        {
            circle.Initialize(rotationAngle, scheme.InitialAngle);
            circle.Selected += OnCircleSelected;
            circle.Rotated += OnCircleRotated;

            _rules.Add(circle, scheme.Rules);
        }

        private void CreateRotatingCircles(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var circleAsset = Resources.Load<GameObject>(AssetsPathGameObject.MosaicPuzzleParts[MosaicPuzzleAssets.RotatingCircle]);

                var circleParentObject = Instantiate(circleAsset, transform);

                var circleObject = circleParentObject.GetComponentInChildren<RotatingCircle>();
                RectTransform rt = circleObject.GetComponent<RectTransform>();
                var size = rt.sizeDelta;
                rt.sizeDelta = new Vector2(size.x - (size.x / count * i), size.y - (size.y / count * i));

                _circles.Add(circleObject);
            }

            var buttonsAsset = Resources.Load<GameObject>(AssetsPathGameObject.MosaicPuzzleParts[MosaicPuzzleAssets.Buttons]);
            var buttonsObject = Instantiate(buttonsAsset, transform);

            _buttons = buttonsObject.GetComponentsInChildren<Button>(true);

            foreach (var button in _buttons)
            {
                button.gameObject.SetActive(false);
                button.onClick.AddListener(() => OnButtonClicked(button));
            }
        }

        private void OnCircleSelected(RotatingCircle selectedCircle)
        {
            _selectedCircle = selectedCircle;
            MoveButtons();
        }

        private void MoveButtons()
        {
            foreach (var button in _buttons)
            {
                RectTransform buttonRt = button.GetComponent<RectTransform>();
                RectTransform circleRt = _selectedCircle.GetComponent<RectTransform>();
                Vector3 newPosition = buttonRt.anchoredPosition;

                newPosition.y = -circleRt.rect.y - BUTTON_OFFSET;
                buttonRt.anchoredPosition = newPosition;

                button.gameObject.SetActive(true);
            }
        }

        private void OnCircleRotated(RotatingCircle circle, bool isRight)
        {
            if (_rules.ContainsKey(circle))
            {
                MoveRuledCircles(_rules[circle], isRight);
                
            }
            CheckComplete();
        }

        private void MoveRuledCircles(Rules[] circleRules, bool isRight)
        {
            foreach (var rule in circleRules)
            {
                _circles[(int)rule.Rule.x].ManualRotate(isRight, (int)rule.Rule.y);
            }
        }

        private void OnButtonClicked(Button button)
        {
            if (button.gameObject.name.Contains(LEFT_IN_NAME))
            {
                _selectedCircle.RotateLeft();
            }
            else
            {
                _selectedCircle.RotateRight();
            }
        }

        #endregion
    }
}