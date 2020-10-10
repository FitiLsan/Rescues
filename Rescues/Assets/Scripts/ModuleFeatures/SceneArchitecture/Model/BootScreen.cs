using DG.Tweening;
using UnityEngine;


namespace Rescues
{
    public class BootScreen : MonoBehaviour, IBootScreen
    {

        #region Fileds
        
        [SerializeField] private float _alphaTweenTime;
        private SpriteRenderer _spriteRenderer;

        #endregion

        
        #region Properties

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

        #endregion
        
        
        #region UnityMethods
        
        public void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        #endregion


        #region Methods
        
        public void CreateFadeEffect(TweenCallback onComplete)
        {
            SpriteAlpha = 0;
            var seq = DOTween.Sequence();
            seq.Append(DOTween.To(() => SpriteAlpha, x => SpriteAlpha = x, 1, _alphaTweenTime)
                .OnComplete(onComplete));
            seq.Append(DOTween.To(() => SpriteAlpha, x => SpriteAlpha = x, 0, _alphaTweenTime));
            seq.Play();
        }
        
        #endregion
        
        
    }
}