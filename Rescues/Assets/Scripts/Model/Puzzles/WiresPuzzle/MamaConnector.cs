using System;
using UnityEngine;


namespace Rescues
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class MamaConnector : MonoBehaviour
    {
        #region Fileds

        public event Action Connected = () => { };

        [SerializeField] private int _applyNumber;
        private int _connectedPapaConnectorHash;

#if UNITY_EDITOR
        private SpriteRenderer _image;
        private Color _baseColor;
#endif

        #endregion


        #region Property

        public bool IsBusy { get; private set; } = false;
        public bool IsCorrectWire { get; private set; } = false;

        public Vector2 Position => transform.position;

        public int ApplyNumber => _applyNumber;

        #endregion


        #region UnityMethods
        
#if UNITY_EDITOR
        private void Awake()
        {
            _image = GetComponent<SpriteRenderer>();
            _baseColor = _image.color;
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
#endif
        
        private void OnTriggerStay2D(Collider2D other)
        {
            Debug.Log("Trigger on enter");
            var papaConnector = other.GetComponent<PapaConnector>();
            if (papaConnector == null) return;

            if (!IsBusy && !papaConnector.IsMoving)
            {
                _connectedPapaConnectorHash = papaConnector.GetHashCode();
                papaConnector.transform.position = transform.position;
                Connect(papaConnector.Number);
            }
            else
            {
                if (papaConnector.GetHashCode() == _connectedPapaConnectorHash)
                {
                    if (IsBusy && papaConnector.IsMoving)
                    {
                        Disconnect();
                    }
                }
            }
            
#if UNITY_EDITOR
            if (IsBusy)
                _image.color = IsCorrectWire == false ? Color.red : Color.green;
            else
                _image.color = _baseColor;
#endif
        }

        #endregion


        #region Methods

        private void Connect(int wireNumber)
        {
            if (IsBusy) return;
            Connected.Invoke();
            IsCorrectWire = _applyNumber == wireNumber ? true : false;
            IsBusy = true;
        }

        private void Disconnect()
        {
            if (!IsBusy) return;
            _connectedPapaConnectorHash = 0;
            IsCorrectWire = false;
            IsBusy = false;
        }

        #endregion
    }
}