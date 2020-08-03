using System;
using UnityEngine;
using UnityEngine.UI;


namespace Rescues
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class MamaConnector : MonoBehaviour
    {
        #region Fileds

        public event Action Connected = () => { };
        
        [SerializeField] private int _applyNumber;
        private int _connectedPapaConnectorHash;

        #endregion


        #region Property

        public bool IsBusy { get; private set; } = false;
        public bool IsCorrectWire { get; private set; } = false;
        
        public Vector2 Position
        {
            get => transform.position;
        }

        public int ApplyNumber
        {
            get => _applyNumber;
            private set => value = _applyNumber;
        }

        #endregion


        #region UnityMethods

        private void OnGUI()
        {
            var image = GetComponent<Image>();
            image.color = IsCorrectWire == false ? Color.gray : Color.green;
        }

        private void OnValidate()
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
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