using System;
using UnityEngine;
using UnityEngine.UI;


namespace Rescues
{
    public class MamaConnector : MonoBehaviour
    {
        #region Fileds

        public event Action Connected = () => { };

        [SerializeField] private int _applyNumber;
        [SerializeField] private Sprite _connected;
        [SerializeField] private Sprite _disconnected;
        private Image _spriteRenderer;
        private int _connectedPapaConnectorHash;
        private PapaConnector _papaConnector;
        
        [NonSerialized]
        public float Radius = 0.5f;
        
        #endregion


        #region Property

        public bool IsBusy { get; private set; } = false;
        public bool IsCorrectWire { get; private set; } = false;

        public Vector2 Position => transform.position;

        public int ApplyNumber => _applyNumber;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _spriteRenderer = gameObject.transform.GetComponent<Image>();
            _spriteRenderer.sprite = _disconnected;
        }

        public void LockAndUnLockPapaConnector(PapaConnector papaConnector)
        {
            if (papaConnector == null) return;

            if (!IsBusy && !papaConnector.IsMoving)
            {
                _papaConnector = papaConnector;
                _connectedPapaConnectorHash = _papaConnector.GetHashCode();
                _spriteRenderer.sprite = _connected;
                Connect(_papaConnector.Number);
            }
            else
            {
                if (papaConnector.GetHashCode() == _connectedPapaConnectorHash)
                {
                    if (IsBusy && _papaConnector.IsMoving)
                    {
                        Disconnect();
                    }
                }
            }
        }

        #endregion


        #region Methods

        private void Connect(int wireNumber)
        {
            if (IsBusy) return;
            _papaConnector.MoveWire(transform.position);
            //тут нужно будет отдельный image с оголенными проводами
            //_papaConnector.SetSpriteConnector(false);
            IsCorrectWire = _applyNumber == wireNumber ? true : false;
            IsBusy = true;
            Connected.Invoke();
        }

        public void Disconnect()
        {
            if (!IsBusy) return;
            //тут нужно будет отдельный image с оголенными проводами
            //_papaConnector.SetSpriteConnector(true);
            _papaConnector = null;
            _spriteRenderer.sprite = _disconnected;
            _connectedPapaConnectorHash = 0;
            IsCorrectWire = false;
            IsBusy = false;
        }

        #endregion
    }
}