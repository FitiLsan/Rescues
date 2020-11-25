using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rescues
{
    public class SafeBitePicBehavior : MonoBehaviour
    {
        [SerializeField] private float timeCount = 1;

        public void Rotate(DirectionOfRotation directionOfRotation)
        {
            var from = transform;
            var to = transform;
            to.rotation = directionOfRotation == DirectionOfRotation.ClockWise ?
                        Quaternion.Euler(to.rotation.eulerAngles.x, to.rotation.eulerAngles.x, to.rotation.eulerAngles.z + 90) :
                        Quaternion.Euler(to.rotation.eulerAngles.x, to.rotation.eulerAngles.x, to.rotation.eulerAngles.z - 90);
            transform.rotation = Quaternion.Slerp(from.rotation, to.rotation, timeCount);
            timeCount = timeCount + Time.deltaTime;

        }

    }
}

