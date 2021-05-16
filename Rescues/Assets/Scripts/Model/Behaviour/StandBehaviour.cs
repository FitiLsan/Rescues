using UnityEngine;


namespace Rescues
{
    public sealed class StandBehaviour: InteractableObjectBehavior
    {
        #region Fields

        public GameObject StandWindow;
        private bool _standActive = false;
        [SerializeField]private BoxCollider2D _boxCollider2D;


        #endregion

        #region UnityMethods


        void Awake()
        {
            _boxCollider2D = this.gameObject.transform.GetComponent<BoxCollider2D>(); //.size = 10f;
        }
        void OnMouseDown()
        {
            //if (_standActive)
            //{
            //    _standActive = false;
            //    StandWindow.SetActive(false);
            //    Debug.Log("neоткрыли");
            //}
            //else
            //{
                //_boxCollider2D = this.gameObject.transform.GetComponent<BoxCollider2D>();
                //_standActive = true;
                StandWindow.SetActive(true);
                Debug.Log("открыли");
                //_boxCollider2D.size = new Vector2(100, 100);
            //}
        }

        #endregion
    }
}
