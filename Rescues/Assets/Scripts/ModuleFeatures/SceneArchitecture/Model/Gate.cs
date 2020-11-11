using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;


namespace Rescues
{
    public class Gate : InteractableObjectBehavior, IGate
    {
        #region Fileds
        [SerializeField, Tooltip("Задержка на локальные перемещения")] public float _localTransferTime = 2f;
        private ITrigger _triggerImplementation;
        
        [NonSerialized] public Action<Gate> GoAction;

        [Header("This Gate")]
        private string _thisLevelName;
        private string _thisLocationName;
        [SerializeField] private int _thisGateId;
        
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

        public float LocalTransferTime => _localTransferTime;
        public int ThisGateId => _thisGateId;
        public string GoToLevelName => _goToLevelName;
        public string GoToLocationName => _goToLocationName;
        public int GoToGateId => _goToGateId;

        #endregion
        

        #region Methods

        private void OnValidate()
        {
            if (gameObject.activeInHierarchy)
                name = _thisGateId + " to " + _goToLevelName + "_" + _goToLocationName + "_" + _goToGateId;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(gameObject.transform.position, _circleCollider.radius);
        }

        [Button("Go by gate way")]
        public void GoByGateWay()
        {
            GoAction?.Invoke(this);
        }

        public void LoadWithTransferTime(Action onLoadComplete)
        {
            StartCoroutine(Transfer(onLoadComplete));
        }

        private IEnumerator Transfer(Action onLoadComplete)
        {
            yield return new WaitForSeconds(_localTransferTime);
            onLoadComplete.Invoke();
            
        }
        
        #endregion
       
        
    }
}