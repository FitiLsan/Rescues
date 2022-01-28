using DG.Tweening;
using System;
using UnityEngine;


namespace Rescues
{
    public class Gate : InteractableObjectBehavior, IGate
    {
        #region Fileds

        [NonSerialized] public Action<Gate, TweenCallback> GoAction;

        [Header("This Gate")]
        private string _thisLevelName;
        private string _thisLocationName;
        [SerializeField] private int _thisGateId;
        [SerializeField] private bool _restartGate;
        [Header("Gate way")]
        [SerializeField] private string _goToLevelName = "Hotel";
        [SerializeField] private string _goToLocationName;
        [SerializeField] private int _goToGateId;

        [SerializeField] private CircleCollider2D _circleCollider;


        #endregion


        #region Private

        public Gate(string levelName, string locationName, int id)
        {
            _thisLevelName = levelName;
            _thisLocationName = locationName;
            _thisGateId = id;
        }

        #endregion


        #region Properties

        public int GoToGateId
        {
            get => _goToGateId;
            set => _goToGateId = value;
        }

        public int ThisGateId => _thisGateId;

        public bool RestartGate => _restartGate;

        public string ThisLevelName
        {
            get => _thisLevelName;
            set => _thisLevelName = value;
        }

        public string ThisLocationName
        {
            get => _thisLocationName;
            set => _thisLocationName = value;
        }

        public string GoToLevelName
        {
            get => _goToLevelName;
            set => _goToLevelName = value;
        }

        public string GoToLocationName
        {
            get => _goToLocationName;
            set => _goToLocationName = value;
        }

        #endregion


        #region Methods

        public void GoByGateWay(TweenCallback AfterTransfer)
        {
            GoAction?.Invoke(this, AfterTransfer);
        }

        #endregion


        #region UnityMethods

        private void OnValidate()
        {
            if (gameObject.activeInHierarchy)
            {
                name = _thisGateId + " to " + _goToLevelName + "_" + _goToLocationName + "_" + _goToGateId;
            }

            Type = InteractableObjectType.Gate;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(gameObject.transform.position, _circleCollider.radius);
        }

        #endregion
    }
}