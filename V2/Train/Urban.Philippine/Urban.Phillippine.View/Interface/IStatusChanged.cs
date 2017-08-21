using System.Collections.Generic;

namespace Urban.Phillippine.View.Interface
{
    public interface IStatusChanged
    {
        void Changed(IDictionary<int, bool> args, int? iPara = null);

        void Changed(IDictionary<int, float> args, int? ipara = null);
    }
}