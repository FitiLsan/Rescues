namespace Rescues
{
    public abstract class Service
    {
        protected readonly Contexts _contexts;

        protected Service(Contexts contexts)
        {
            _contexts = contexts;
        }
    }
}
