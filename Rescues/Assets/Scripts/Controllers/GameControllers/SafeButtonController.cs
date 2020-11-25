using System;
using System.Collections.Generic;
using UnityEngine;


namespace Rescues
{
    public class SafeButtonController : ButtonBehavior
    {
        #region Serializable

        [SerializeField] private List<SafeBitePicBehavior> _bitesOfPics;
        [SerializeField] private DirectionOfRotation _directionOfRotationPics = DirectionOfRotation.ClockWise;

        #endregion

        #region Properties

        public override Vector3 Position { get => transform.position; }

        public override Collider2D Collider { get => GetComponent<Collider2D>(); }

        #endregion

        #region Methods

        public override void Click()
        {
            RotatePics();
        }

        private void RotatePics()
        {
            foreach (var _biteOfPic in _bitesOfPics)
            {
                _biteOfPic.GetComponent<SafeBitePicBehavior>().Rotate(_directionOfRotationPics);
            }
        }

        #endregion



    }
}
