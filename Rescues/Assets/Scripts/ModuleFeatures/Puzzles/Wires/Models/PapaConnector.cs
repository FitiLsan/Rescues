using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Rescues
{
    public class PapaConnector : MonoBehaviour,IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        #region Fileds

        [SerializeField] private Sprite _connectorSprite;
        private Wire _wire;
        private bool _isMoving = false;
        private Image _image;
        
        public List<MamaConnector> mamaZonePositions = new List<MamaConnector>();
        public event Action<MamaConnector,PapaConnector> InMamaConnectorZone;
        
        #endregion

        
        #region Properties

        public int Number => _wire.Number;
        public bool IsMoving => _isMoving;

        #endregion


        #region UnityMethods
        
        private void Awake()
        {
            _wire = GetComponentInParent<Wire>();
            _image = GetComponent<Image>();
            if (_connectorSprite)
                _image.sprite = _connectorSprite;
        }
        
        #endregion

        
        #region IDrag

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isMoving = true;
            _wire.SetEndPointRemeber();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            var mamaConnector = IsThisPapaConnectorInMamaConnectorZone();
            _isMoving = false;
            if (mamaConnector)
                InMamaConnectorZone.Invoke(mamaConnector,this);
        }

        public void OnDrag(PointerEventData eventData)
        {
            var mamaConnector = IsThisPapaConnectorInMamaConnectorZone();
            if (mamaConnector)
                InMamaConnectorZone.Invoke(mamaConnector,this);
            var cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MoveWire(cursorPosition);
        }

        #endregion

        
        #region Methods
        
        public void MoveWire(Vector2 newPosition) => _wire.MoveWire(newPosition);

        public void SetSpriteConnector(bool value) => _image.enabled = value;

        private MamaConnector IsThisPapaConnectorInMamaConnectorZone()
        {
            var position = transform.position;
          
            foreach (var mamaConnector in mamaZonePositions)
            {
                var mamaPosition = mamaConnector.transform.position;
                double d = Math.Sqrt(Math.Pow(position.x - mamaPosition.x, 2) 
                                     + Math.Pow(position.y - mamaPosition.y, 2));
                if (d <= mamaConnector.Radius)
                    return mamaConnector;
            }
            return null;
        }

        #endregion
        
    }
}