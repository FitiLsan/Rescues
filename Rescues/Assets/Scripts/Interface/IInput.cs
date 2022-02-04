using System;

namespace Rescues
{
    public interface IInput : IDisposable
    {
        string GetSaveString();
        void RebindFromString(string rebindString);
    }
}