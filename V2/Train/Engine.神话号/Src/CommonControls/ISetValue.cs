using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonControls
{
    public interface ISetValue
    {
        ISetValue LastISetValue { get; set; }
        ISetValue NextISetValue { get; set; }

        Int32 CurrentLabelID { get; set; }

        void Last();

        void Next();

        void ToTheLast();
        void ToTheFirst();

        Int32 GetValue();

        void SetValue(Int32 value);
    }
}
