﻿using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Rescues
{
    public sealed class GameController : UpdateListCreator
    {
        #region Fields
        
        private GameStateController _activeController;

        #endregion
        
 
        #region UnityMethods

        private void Awake()
        {
        
        }

        private void Start()
        {
            GameContext context = new GameContext();
            Services services = Services.SharedInstance;
            services.Initialize(context);
            
            _activeController = new GameSystemsController(context, services);
            _activeController.Initialize();
        }

        private void FixedUpdate()
        {
            _activeController.Execute(UpdateType.Fixed);
            _activeController.Cleanup(UpdateType.Fixed);
        }
          
        //private void Update()
        //{
        //    _activeController.Execute(UpdateType.Update);
        //    _activeController.Cleanup(UpdateType.Update);
        //}

        public override void GameControllerScriptFunctions()
        {
            _activeController.Execute(UpdateType.Update);
            _activeController.Cleanup(UpdateType.Update);
        }

        private void LateUpdate()
        {
            _activeController.Execute(UpdateType.Late);
            _activeController.Cleanup(UpdateType.Update);
        }

        private void OnDestroy()
        {
            _activeController.TearDown();
        }

        #if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            if (!EditorApplication.isPlaying)
            {
                return;
            }
            _activeController.Execute(UpdateType.Gizmos);
            _activeController.Cleanup(UpdateType.Gizmos);
        }

        #endif

        #endregion
    }
}
