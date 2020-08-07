using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
