namespace Rescues
{
    public interface IPuzzle
    {
        void Activate();
        void Close();
        void Finish();
        void ResetValues();
    }
}