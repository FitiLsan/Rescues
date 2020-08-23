using UnityEngine;


namespace Rescues
{
    public class Wire : MonoBehaviour
    {
        #region Fileds

        private const float MIDDLE_X_DIVIDDER = 6;
        private const float MIDDLE_Y_DIVIDDER = 2;
        private const int COUNT_WIRE_PARTS = 3;
        
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _middlePoint;
        [SerializeField] private Transform _endPoint;
        [SerializeField] private int _number;

        private LineRenderer _lineRenderer;
        private Vector3 _endPointRemeber = Vector3.zero;
        
        #endregion


        #region Properties

        public int Number => _number;


#endregion

        #region UnityMethods

        private void OnValidate()
        {
            var collider = _endPoint.GetComponent<BoxCollider2D>();
            if (collider == null)
                _endPoint.gameObject.AddComponent<BoxCollider2D>();
        }

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.positionCount = COUNT_WIRE_PARTS;
        }

        private void FixedUpdate()
        {
            _lineRenderer.SetPosition(0, _startPoint.position);
            _lineRenderer.SetPosition(1, _middlePoint.position);
            _lineRenderer.SetPosition(2, _endPoint.position);
        }

        #endregion

        //TODO Сделать так, чтобы провода нельзя было вытащить за игровую поверхность пазла

        #region Methods

        public void SetEndPointRemeber()
        {
            _endPointRemeber = _endPoint.position;
        }
        
        public void MoveWire(Vector3 cursorPosition)
        {
            _endPoint.position = cursorPosition;
            var positionDelta = _endPoint.position - _endPointRemeber;
            var newMiddlePosition = _middlePoint.localPosition;
            newMiddlePosition.x += positionDelta.x / MIDDLE_X_DIVIDDER;
            newMiddlePosition.y += positionDelta.y / MIDDLE_Y_DIVIDDER;
            _middlePoint.localPosition = newMiddlePosition;
            _endPointRemeber = _endPoint.position;
        }

        #endregion
    }
}