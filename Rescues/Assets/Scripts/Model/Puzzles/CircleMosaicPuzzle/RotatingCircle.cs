using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace Rescues
{
    public class RotatingCircle : MonoBehaviour, IPointerClickHandler
    {

        #region Fields

        private const float FULL_CIRCLE = 360.0f;

        public event Action<RotatingCircle, bool> Rotated = (circle, isRight) => { };
        public event Action<RotatingCircle> Selected = (circle) => { };

        private Vector3 _rotation;
        private Button[] _buttons;
        private int _currentAngle;
        private int _rotatingAngle;

        public Button[] Buttons { get => _buttons; set => _buttons = value; }
        public int Angle => _currentAngle;

        #endregion


        #region Methods

        public void Initialize(int angle, int initialAngle)
        {
            _rotatingAngle = angle;
            _currentAngle = initialAngle;

            _rotation = Vector3.zero;
            _rotation.z = _currentAngle;
            transform.Rotate(_rotation);
        }

        public void RotateRight()
        {
            Rotate(-_rotatingAngle);
            Rotated.Invoke(this, true);
        }

        public void RotateLeft()
        {
            Rotate(_rotatingAngle);
            Rotated.Invoke(this, false);
        }

        public void ManualRotate(bool isRight, int steps)
        {
            int direction = isRight ? -1 : 1;
            Rotate(_rotatingAngle * steps * direction);
        }

        private void Rotate(int angle)
        {
            transform.Rotate(0,0, angle);
            _currentAngle = (int)transform.rotation.eulerAngles.z;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Selected.Invoke(this);
        }

        public bool IsComplete()
        {
            return Mathf.Abs(Angle % FULL_CIRCLE) == 0;
        }

        #endregion

    }
}