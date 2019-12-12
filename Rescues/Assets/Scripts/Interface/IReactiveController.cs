namespace Rescues
{
    public interface IReactiveController : IExecuteController
    {
        void Activate();

        void Deactivate();

        void Clear();
    }
}
