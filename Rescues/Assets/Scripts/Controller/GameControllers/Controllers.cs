using System.Collections.Generic;


namespace Rescues
{
    public abstract class Controllers : IInitializeController, IExecuteController, ICleanupController, ITearDownController
    {
        protected readonly List<IInitializeController> _initializeSystems;
        protected readonly List<IExecuteController> _executeSystems;
        protected readonly List<ICleanupController> _cleanupSystems;
        protected readonly List<ITearDownController> _tearDownSystems;

        protected Controllers()
        {
            _initializeSystems = new List<IInitializeController>();
            _executeSystems = new List<IExecuteController>();
            _cleanupSystems = new List<ICleanupController>();
            _tearDownSystems = new List<ITearDownController>();
        }

        protected virtual Controllers Add(IController controller)
        {
            switch (controller)
            {
                case ICleanupController cleanupController:
                    _cleanupSystems.Add(cleanupController);
                    break;
                case IExecuteController executeController:
                    _executeSystems.Add(executeController);
                    break;
                case IInitializeController initializeController:
                    _initializeSystems.Add(initializeController);
                    break;
                case ITearDownController tearDownController:
                    _tearDownSystems.Add(tearDownController);
                    break;
            }

            return this;
        }

        public virtual void Initialize()
        {
            for (var index = 0; index < _initializeSystems.Count; ++index)
            {
                _initializeSystems[index].Initialize();
            }
        }

        public virtual void Execute()
        {
            for (var index = 0; index < _executeSystems.Count; ++index)
            {
                _executeSystems[index].Execute();
            }
        }

        public virtual void Cleanup()
        {
            for (var index = 0; index < _cleanupSystems.Count; ++index)
            {
                _cleanupSystems[index].Cleanup();
            }
        }
    
        public virtual void TearDown()
        {
            for (var index = 0; index < _tearDownSystems.Count; ++index)
            {
                _tearDownSystems[index].TearDown();
            }
        }
    }
}
