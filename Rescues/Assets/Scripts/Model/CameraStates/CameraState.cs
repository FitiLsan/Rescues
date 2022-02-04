namespace Rescues
{
    public abstract class CameraState
    {
        #region Fields

        protected CameraData _activeCamera;

        #endregion


        #region Methods

        public abstract void InitState(CameraData activeCamera);

        public abstract void UpdateState();

        #endregion
    }
}