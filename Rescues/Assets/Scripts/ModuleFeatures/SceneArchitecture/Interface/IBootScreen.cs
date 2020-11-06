using DG.Tweening;
using UnityEngine;


namespace Rescues
{
    public interface IBootScreen
    {
        void ShowBootScreen(Services services, TweenCallback onComplete);
        void Destroy();
    }
}