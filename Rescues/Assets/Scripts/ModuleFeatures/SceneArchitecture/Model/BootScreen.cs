using DG.Tweening;
using UnityEngine;


namespace Rescues
{
    public class BootScreen : MonoBehaviour, IBootScreen
    {
        
        [SerializeField] private float _alphaTweenTime;
        private SpriteRenderer _spriteRenderer;

        public void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        private float SpriteAlpha
        {
            get => _spriteRenderer.color.a;
            set
            {
                var color = _spriteRenderer.color;
                color.a = value;
                _spriteRenderer.color = color;
            }
        }
        
        public void CreateFadeEffect(TweenCallback onComplete)
        {
            SpriteAlpha = 0;
            var seq = DOTween.Sequence();
            seq.Append(DOTween.To(() => SpriteAlpha, x => SpriteAlpha = x, 1, _alphaTweenTime)
                .OnComplete(onComplete));
            seq.Append(DOTween.To(() => SpriteAlpha, x => SpriteAlpha = x, 0, _alphaTweenTime));
            seq.Play();
        }
    }
}