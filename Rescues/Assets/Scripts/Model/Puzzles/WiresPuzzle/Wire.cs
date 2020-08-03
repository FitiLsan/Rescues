using UnityEngine;
using UnityEngine.EventSystems;


namespace Rescues
{
    public class Wire : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {

        #region Fileds
        
        public Transform StartPoint;
        public Transform EndPoint;
        
        [SerializeField] private int _number;
        private bool _isMoving = false;

        #endregion


        #region Properties

        public int Number
        {
            get => _number;
            private set => value = _number;
        }

        public bool IsConected { get; private set; }

        public bool IsMoving
        {
            get => _isMoving;
            private set => _isMoving = value;
        }

        #endregion

        #region UnityMethods

        private void OnValidate()
        {
            var collider = EndPoint.GetComponent<BoxCollider2D>();
            if (collider == null)
                EndPoint.gameObject.AddComponent<BoxCollider2D>();
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(StartPoint.position, EndPoint.position);
        }


        #endregion

        //TODO Сделать так, чтобы провода нельзя было вытащить за игровую поверхность пазла
        #region Methods

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isMoving = true;
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            EndPoint.position = Input.mousePosition;
            
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            _isMoving = false;
        }

        #endregion
      
    }
}