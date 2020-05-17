using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rescues
{
    public class FogOfWarBehaviour : MonoBehaviour
    {
        #region PrivateData
        public bool IsVisible = false;
        [SerializeField] private Color _visibleColor = new Color(255, 255, 255, 0);
        [SerializeField] private Color _hiddenColor = new Color(1f, 1f, 1f, 0.5f);
        [SerializeField] private Color _startColor = new Color(0, 0, 0, 255);
        private SpriteMask _mask;
        private Renderer _renderer;

        #endregion
        void Start()
        {
            _renderer = GetComponent<Renderer>();
            _renderer.material.color = _startColor;
            _mask = GetComponentInChildren<SpriteMask>();
            //_mask.enabled = false;
        }

        public void FogEnter()
        {
            _renderer.material.color = _visibleColor;
            IsVisible = true;
            //_mask.enabled = true;
        }

        public void FogLeft()
        {
            _renderer.material.color = _hiddenColor;
            IsVisible = false;
            //_mask.enabled = false;
        }
    }
}
